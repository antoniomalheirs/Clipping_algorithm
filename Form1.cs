namespace DesenhaPrimitivas
{
    public partial class Form1 : Form
    {

        private List<Point> Poligono = new List<Point>();
        private List<Rectangle> Retangulo = new List<Rectangle>();

        private Point PI;
        private Point PF;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private bool Dentrojanela(Point ponto, Point janelaMin, Point janelaMax)
        {
            return (ponto.X >= janelaMin.X && ponto.X <= janelaMax.X) &&
                   (ponto.Y >= janelaMin.Y && ponto.Y <= janelaMax.Y);
        }

        [Flags]
        public enum Codigo
        {
            Dentro = 0, // 0000
            Esquerda = 1,   // 0001
            Direira = 2,  // 0010
            Baixo = 4, // 0100
            Cima = 8     // 1000
        }

        public Codigo Retornacodigo(Point p, Point janelaMin, Point janelaMax)
        {
            Codigo codigo = Codigo.Dentro;

            if (p.X < janelaMin.X)
                codigo |= Codigo.Esquerda;
            else if (p.X > janelaMax.X)
                codigo |= Codigo.Direira;

            if (p.Y < janelaMin.Y)
                codigo |= Codigo.Baixo;
            else if (p.Y > janelaMax.Y)
                codigo |= Codigo.Cima;

            return codigo;
        }

        public bool CohenSutherlandClip(ref Point p1, ref Point p2, Point janelaMin, Point janelaMax)
        {
            Codigo codeP1 = Retornacodigo(p1, janelaMin, janelaMax);
            Codigo codeP2 = Retornacodigo(p2, janelaMin, janelaMax);

            while (true)
            {
                if ((codeP1 | codeP2) == Codigo.Dentro)
                    return true;
                else if ((codeP1 & codeP2) != 0)
                    return false;

                Codigo Code = codeP1 != Codigo.Dentro ? codeP1 : codeP2;
                Point intersec = new Point();

                if ((Code & Codigo.Cima) != 0)
                {
                    intersec.X = p1.X + (p2.X - p1.X) * (janelaMax.Y - p1.Y) / (p2.Y - p1.Y);
                    intersec.Y = janelaMax.Y;
                }
                else if ((Code & Codigo.Baixo) != 0)
                {
                    intersec.X = p1.X + (p2.X - p1.X) * (janelaMin.Y - p1.Y) / (p2.Y - p1.Y);
                    intersec.Y = janelaMin.Y;
                }
                else if ((Code & Codigo.Direira) != 0)
                {
                    intersec.Y = p1.Y + (p2.Y - p1.Y) * (janelaMax.X - p1.X) / (p2.X - p1.X);
                    intersec.X = janelaMax.X;
                }
                else if ((Code & Codigo.Esquerda) != 0)
                {
                    intersec.Y = p1.Y + (p2.Y - p1.Y) * (janelaMin.X - p1.X) / (p2.X - p1.X);
                    intersec.X = janelaMin.X;
                }

                if (Code == codeP1)
                {
                    p1 = intersec;
                    codeP1 = Retornacodigo(p1, janelaMin, janelaMax);
                }
                else
                {
                    p2 = intersec;
                    codeP2 = Retornacodigo(p2, janelaMin, janelaMax);
                }
            }
        }

        private List<Point> recortaPoligono(List<Point> poligono, Point janelaMin, Point janelaMax)
        {
            List<Point> recortado = new List<Point>();

            for (int i = 0; i < poligono.Count; i++)
            {
                int j = (i + 1) % poligono.Count;

                Point p1 = poligono[i];
                Point p2 = poligono[j];

                bool p1Inside = Dentrojanela(p1, janelaMin, janelaMax);
                bool p2Inside = Dentrojanela(p2, janelaMin, janelaMax);

                if (p1Inside && p2Inside)
                {
                    recortado.Add(p1);
                }
                else if (p1Inside && !p2Inside)
                {
                    recortado.Add(p1);
                    CohenSutherlandClip(ref p1, ref p2, janelaMin, janelaMax); // Use 'ref' aqui
                    recortado.Add(p1);
                }
                else if (!p1Inside && p2Inside)
                {
                    CohenSutherlandClip(ref p1, ref p2, janelaMin, janelaMax); // Use 'ref' aqui
                    recortado.Add(p1);
                }
            }

            return recortado;
        }

        private List<Point> recortaRetangulo(Point p1, Point p2, Point p3, Point p4, Point janelaMin, Point janelaMax)
        {
            List<Point> pontosRecortados = new List<Point>();
            List<Point> pontosRetangulo = new List<Point> { p1, p2, p3, p4 };

            for (int i = 0; i < pontosRetangulo.Count; i++)
            {
                int j = (i + 1) % pontosRetangulo.Count;

                Point pontoAtual = pontosRetangulo[i];
                Point pontoProximo = pontosRetangulo[j];

                bool atualDentro = Dentrojanela(pontoAtual, janelaMin, janelaMax);
                bool proximoDentro = Dentrojanela(pontoProximo, janelaMin, janelaMax);

                if (atualDentro && proximoDentro)
                {
                    pontosRecortados.Add(pontoAtual);
                }
                else if (atualDentro && !proximoDentro)
                {
                    pontosRecortados.Add(pontoAtual);
                    CohenSutherlandClip(ref pontoAtual, ref pontoProximo, janelaMin, janelaMax);
                    pontosRecortados.Add(pontoAtual);
                }
                else if (!atualDentro && proximoDentro)
                {
                    CohenSutherlandClip(ref pontoAtual, ref pontoProximo, janelaMin, janelaMax);
                    pontosRecortados.Add(pontoAtual);
                }
            }

            return pontosRecortados;
        }

        private void desenhapoligono(Graphics graphics, List<Point> poligonoRecortado, Panel panel)
        {
            if (poligonoRecortado.Count > 1)
            {
                Poligono poligono = new Poligono();
                poligono.DesenhaForma(graphics, poligonoRecortado.ToArray(), panel);
                poligono.PreencheForma(graphics, poligonoRecortado.ToArray(), panel);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                List<Point> poligonoRecortado = recortaPoligono(Poligono, panel1.ClientRectangle.Location, new Point(panel1.ClientRectangle.Right, panel1.ClientRectangle.Bottom));
                desenhapoligono(graphics, poligonoRecortado, panel4);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Poligono.Add(e.Location);
            panel4.Invalidate();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (PI == Point.Empty)
            {
                PI = e.Location;
            }
            else if (PF == Point.Empty)
            {
                PF = e.Location;
                Rectangle retangulo = new Rectangle(PI.X, PI.Y, PF.X - PI.X, PF.Y - PI.Y);
                Retangulo.Add(retangulo);
                PI = Point.Empty;
                PF = Point.Empty;
                panel3.Invalidate();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                foreach (var retangulo in Retangulo)
                {
                    int x = Math.Min(retangulo.Left, retangulo.Right);
                    int y = Math.Min(retangulo.Top, retangulo.Bottom);
                    int width = Math.Abs(retangulo.Right - retangulo.Left);
                    int height = Math.Abs(retangulo.Bottom - retangulo.Top);

                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + width, y);
                    Point p3 = new Point(x + width, y + height);
                    Point p4 = new Point(x, y + height);

                    Point janelaMin = new Point(0, 0);
                    Point janelaMax = new Point(panel2.Width, panel2.Height);

                    List<Point> retanguloRecortado = recortaRetangulo(p1, p2, p3, p4, janelaMin, janelaMax);

                    if (retanguloRecortado.Count > 2)
                    {
                        Retangulo ret = new Retangulo();
                        ret.DesenhaForma(graphics, retanguloRecortado[0], retanguloRecortado[2], panel2);
                        ret.PreencheForma(graphics, retanguloRecortado[0], retanguloRecortado[2], panel2);
                    }
                }
            }
        }


    }
}
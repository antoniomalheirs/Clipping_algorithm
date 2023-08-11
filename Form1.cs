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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                if (Poligono.Count > 1)
                {
                    Poligono poligono = new Poligono();
                    poligono.DesenhaForma(graphics, Poligono.ToArray());
                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Poligono.Add(e.Location);
            panel1.Invalidate();
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
                panel2.Invalidate();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                foreach (var retangulo in Retangulo)
                {
                    Retangulo ret = new Retangulo();
                    ret.DesenhaForma(graphics, new Point(retangulo.Left, retangulo.Top), new Point(retangulo.Right, retangulo.Bottom));
                }
            }
        }
    }
}
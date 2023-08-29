using System.Drawing;
using System.Windows.Forms;

namespace DesenhaPrimitivas
{
    internal class Retangulo : Desenha
    {
        int x;
        int y;
        int width;
        int height;

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

        public void DesenhaForma(Graphics graphics, Point ponto1, Point ponto2, Panel panel)
        {
            this.x = Math.Min(ponto1.X, ponto2.X);
            this.y = Math.Min(ponto1.Y, ponto2.Y);
            this.width = Math.Abs(ponto2.X - ponto1.X);
            this.height = Math.Abs(ponto2.Y - ponto1.Y);

            Point windowMin = new Point(panel.ClientRectangle.Left, panel.ClientRectangle.Top);
            Point windowMax = new Point(panel.ClientRectangle.Right, panel.ClientRectangle.Bottom);

            Point p1 = new Point(x, y);
            Point p2 = new Point(x + width, y + height);

            if (CohenSutherlandClip(ref p1, ref p2, windowMin, windowMax))
            {
                x = p1.X;
                y = p1.Y;
                width = p2.X - p1.X;
                height = p2.Y - p1.Y;
                graphics.DrawRectangle(caneta, x, y, width, height);
            }
        }

        public void PreencheForma(Graphics graphics, Point ponto1, Point ponto2, Panel panel)
        {
            this.x = Math.Min(ponto1.X, ponto2.X);
            this.y = Math.Min(ponto1.Y, ponto2.Y);
            this.width = Math.Abs(ponto2.X - ponto1.X);
            this.height = Math.Abs(ponto2.Y - ponto1.Y);

            Point windowMin = new Point(panel.ClientRectangle.Left, panel.ClientRectangle.Top);
            Point windowMax = new Point(panel.ClientRectangle.Right, panel.ClientRectangle.Bottom);

            Point p1 = new Point(x, y);
            Point p2 = new Point(x + width, y + height);

            if (CohenSutherlandClip(ref p1, ref p2, windowMin, windowMax))
            {
                x = p1.X;
                y = p1.Y;
                width = p2.X - p1.X;
                height = p2.Y - p1.Y;
                graphics.FillRectangle(caneta2, x + 1, y + 1, width - 1, height - 1);
            }
        }
    }
}

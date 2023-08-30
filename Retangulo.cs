using System.Drawing;
using System.Windows.Forms;

namespace DesenhaPrimitivas
{
    internal class Retangulo : Desenha
    {
        public int x;
        public int y;
        public int largura;
        public int altura;

        [Flags]
        private enum Codigo
        {
            Dentro = 0,
            Esquerda = 1,
            Direira = 2,
            Baixo = 4,
            Cima = 8
        }

        private Codigo RetornaCodigo(Point p, Point janelaMin, Point janelaMax)
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

        private bool CohenSutherlandClip(ref Point p1, ref Point p2, Point janelaMin, Point janelaMax)
        {
            Codigo codigoP1 = RetornaCodigo(p1, janelaMin, janelaMax);
            Codigo codigoP2 = RetornaCodigo(p2, janelaMin, janelaMax);

            while (true)
            {
                if ((codigoP1 | codigoP2) == Codigo.Dentro)
                    return true;
                else if ((codigoP1 & codigoP2) != 0)
                    return false;

                Codigo codigo = codigoP1 != Codigo.Dentro ? codigoP1 : codigoP2;
                Point intersecao = new Point();

                if ((codigo & Codigo.Cima) != 0)
                {
                    intersecao.X = p1.X + (p2.X - p1.X) * (janelaMax.Y - p1.Y) / (p2.Y - p1.Y);
                    intersecao.Y = janelaMax.Y;
                }
                else if ((codigo & Codigo.Baixo) != 0)
                {
                    intersecao.X = p1.X + (p2.X - p1.X) * (janelaMin.Y - p1.Y) / (p2.Y - p1.Y);
                    intersecao.Y = janelaMin.Y;
                }
                else if ((codigo & Codigo.Direira) != 0)
                {
                    intersecao.Y = p1.Y + (p2.Y - p1.Y) * (janelaMax.X - p1.X) / (p2.X - p1.X);
                    intersecao.X = janelaMax.X;
                }
                else if ((codigo & Codigo.Esquerda) != 0)
                {
                    intersecao.Y = p1.Y + (p2.Y - p1.Y) * (janelaMin.X - p1.X) / (p2.X - p1.X);
                    intersecao.X = janelaMin.X;
                }

                if (codigo == codigoP1)
                {
                    p1 = intersecao;
                    codigoP1 = RetornaCodigo(p1, janelaMin, janelaMax);
                }
                else
                {
                    p2 = intersecao;
                    codigoP2 = RetornaCodigo(p2, janelaMin, janelaMax);
                }
            }
        }

        public void DesenhaForma(Graphics graphics, Point ponto1, Point ponto2, Panel painel)
        {
            this.x = Math.Min(ponto1.X, ponto2.X);
            this.y = Math.Min(ponto1.Y, ponto2.Y);
            this.largura = Math.Abs(ponto2.X - ponto1.X);
            this.altura = Math.Abs(ponto2.Y - ponto1.Y);

            Point janelaMin = new Point(painel.ClientRectangle.Left, painel.ClientRectangle.Top);
            Point janelaMax = new Point(painel.ClientRectangle.Right, painel.ClientRectangle.Bottom);

            Point p1 = new Point(x, y);
            Point p2 = new Point(x + largura, y + altura);

            if (CohenSutherlandClip(ref p1, ref p2, janelaMin, janelaMax))
            {
                x = p1.X;
                y = p1.Y;
                largura = p2.X - p1.X;
                altura = p2.Y - p1.Y;

                graphics.DrawRectangle(caneta, x, y, largura, altura);
            }
        }

        public void PreencheForma(Graphics graphics, Point ponto1, Point ponto2, Panel painel)
        {
            this.x = Math.Min(ponto1.X, ponto2.X);
            this.y = Math.Min(ponto1.Y, ponto2.Y);
            this.largura = Math.Abs(ponto2.X - ponto1.X);
            this.altura = Math.Abs(ponto2.Y - ponto1.Y);

            Point janelaMin = new Point(painel.ClientRectangle.Left, painel.ClientRectangle.Top);
            Point janelaMax = new Point(painel.ClientRectangle.Right, painel.ClientRectangle.Bottom);

            Point p1 = new Point(x, y);
            Point p2 = new Point(x + largura, y + altura);

            if (CohenSutherlandClip(ref p1, ref p2, janelaMin, janelaMax))
            {
                x = p1.X;
                y = p1.Y;
                largura = p2.X - p1.X;
                altura = p2.Y - p1.Y;

                graphics.FillRectangle(caneta2, x + 1, y + 1, largura - 1, altura - 1);
            }
        }
    }
}

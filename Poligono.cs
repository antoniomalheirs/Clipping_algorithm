using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace DesenhaPrimitivas
{
    internal class Poligono : Desenha
    {
        Point[] pontos;

        private Point recortaPonto(Point ponto, Point janelaMin, Point janelaMax)
        {
            if (ponto.X < janelaMin.X)
                ponto.X = janelaMin.X;
            else if (ponto.X > janelaMax.X)
                ponto.X = janelaMax.X;

            if (ponto.Y < janelaMin.Y)
                ponto.Y = janelaMin.Y;
            else if (ponto.Y > janelaMax.Y)
                ponto.Y = janelaMax.Y;

            return ponto;
        }

        private List<Point> recortaPontos(List<Point> pontos, Point janelaMin, Point janelaMax)
        {
            List<Point> pontosRecortados = new List<Point>();

            foreach (Point point in pontos)
            {
                Point pontosrecortados = recortaPonto(point, janelaMin, janelaMax);
                pontosRecortados.Add(pontosrecortados);
            }

            return pontosRecortados;
        }

        public void DesenhaForma(Graphics graphics, Point[] ponto, Panel panel)
        {
            this.pontos = recortaPontos(ponto.ToList(), panel.ClientRectangle.Location, new Point(panel.ClientRectangle.Right, panel.ClientRectangle.Bottom)).ToArray();
            graphics.DrawPolygon(caneta, pontos);
        }

        public void PreencheForma(Graphics graphics, Point[] ponto, Panel panel)
        {
            this.pontos = recortaPontos(ponto.ToList(), panel.ClientRectangle.Location, new Point(panel.ClientRectangle.Right, panel.ClientRectangle.Bottom)).ToArray();
            graphics.FillPolygon(caneta2, pontos);
        }

    }
}

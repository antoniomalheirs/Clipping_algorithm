using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace DesenhaPrimitivas
{
    internal class Poligono : Desenha
    {
        Point[] pontos;

        private Point ClipPoint(Point point, Point windowMin, Point windowMax)
        {
            if (point.X < windowMin.X)
                point.X = windowMin.X;
            else if (point.X > windowMax.X)
                point.X = windowMax.X;

            if (point.Y < windowMin.Y)
                point.Y = windowMin.Y;
            else if (point.Y > windowMax.Y)
                point.Y = windowMax.Y;

            return point;
        }

        private List<Point> ClipPoints(List<Point> points, Point windowMin, Point windowMax)
        {
            List<Point> clippedPoints = new List<Point>();

            foreach (Point point in points)
            {
                Point clippedPoint = ClipPoint(point, windowMin, windowMax);
                clippedPoints.Add(clippedPoint);
            }

            return clippedPoints;
        }

        public void DesenhaForma(Graphics graphics, Point[] ponto, Panel panel)
        {
            this.pontos = ClipPoints(ponto.ToList(), panel.ClientRectangle.Location, new Point(panel.ClientRectangle.Right, panel.ClientRectangle.Bottom)).ToArray();
            graphics.DrawPolygon(caneta, pontos);
        }

        public void PreencheForma(Graphics graphics, Point[] ponto, Panel panel)
        {
            this.pontos = ClipPoints(ponto.ToList(), panel.ClientRectangle.Location, new Point(panel.ClientRectangle.Right, panel.ClientRectangle.Bottom)).ToArray();
            graphics.FillPolygon(caneta2, pontos);
        }

    }
}

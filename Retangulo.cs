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
        public enum OutCode
        {
            Inside = 0, // 0000
            Left = 1,   // 0001
            Right = 2,  // 0010
            Bottom = 4, // 0100
            Top = 8     // 1000
        }

        public OutCode ComputeOutCode(Point p, Point windowMin, Point windowMax)
        {
            OutCode code = OutCode.Inside;

            if (p.X < windowMin.X)
                code |= OutCode.Left;
            else if (p.X > windowMax.X)
                code |= OutCode.Right;

            if (p.Y < windowMin.Y)
                code |= OutCode.Bottom;
            else if (p.Y > windowMax.Y)
                code |= OutCode.Top;

            return code;
        }

        public bool CohenSutherlandClip(ref Point p1, ref Point p2, Point windowMin, Point windowMax)
        {
            OutCode outCodeP1 = ComputeOutCode(p1, windowMin, windowMax);
            OutCode outCodeP2 = ComputeOutCode(p2, windowMin, windowMax);

            while (true)
            {
                if ((outCodeP1 | outCodeP2) == OutCode.Inside)
                    return true;
                else if ((outCodeP1 & outCodeP2) != 0)
                    return false;
                OutCode outCode = outCodeP1 != OutCode.Inside ? outCodeP1 : outCodeP2;
                Point intersection = new Point();

                if ((outCode & OutCode.Top) != 0)
                {
                    intersection.X = p1.X + (p2.X - p1.X) * (windowMax.Y - p1.Y) / (p2.Y - p1.Y);
                    intersection.Y = windowMax.Y;
                }
                else if ((outCode & OutCode.Bottom) != 0)
                {
                    intersection.X = p1.X + (p2.X - p1.X) * (windowMin.Y - p1.Y) / (p2.Y - p1.Y);
                    intersection.Y = windowMin.Y;
                }
                else if ((outCode & OutCode.Right) != 0)
                {
                    intersection.Y = p1.Y + (p2.Y - p1.Y) * (windowMax.X - p1.X) / (p2.X - p1.X);
                    intersection.X = windowMax.X;
                }
                else if ((outCode & OutCode.Left) != 0)
                {
                    intersection.Y = p1.Y + (p2.Y - p1.Y) * (windowMin.X - p1.X) / (p2.X - p1.X);
                    intersection.X = windowMin.X;
                }

                if (outCode == outCodeP1)
                {
                    p1 = intersection;
                    outCodeP1 = ComputeOutCode(p1, windowMin, windowMax);
                }
                else
                {
                    p2 = intersection;
                    outCodeP2 = ComputeOutCode(p2, windowMin, windowMax);
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

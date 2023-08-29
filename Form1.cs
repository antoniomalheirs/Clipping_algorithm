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

        private bool IsInsideWindow(Point point, Point windowMin, Point windowMax)
        {
            return (point.X >= windowMin.X && point.X <= windowMax.X) &&
                   (point.Y >= windowMin.Y && point.Y <= windowMax.Y);
        }

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

        private List<Point> ClipPolygon(List<Point> polygon, Point windowMin, Point windowMax)
        {
            List<Point> clippedPolygon = new List<Point>();

            for (int i = 0; i < polygon.Count; i++)
            {
                int j = (i + 1) % polygon.Count;

                Point p1 = polygon[i];
                Point p2 = polygon[j];

                bool p1Inside = IsInsideWindow(p1, windowMin, windowMax);
                bool p2Inside = IsInsideWindow(p2, windowMin, windowMax);

                if (p1Inside && p2Inside)
                {
                    clippedPolygon.Add(p1);
                }
                else if (p1Inside && !p2Inside)
                {
                    clippedPolygon.Add(p1);
                    CohenSutherlandClip(ref p1, ref p2, windowMin, windowMax); // Use 'ref' aqui
                    clippedPolygon.Add(p1);
                }
                else if (!p1Inside && p2Inside)
                {
                    CohenSutherlandClip(ref p1, ref p2, windowMin, windowMax); // Use 'ref' aqui
                    clippedPolygon.Add(p1);
                }
            }

            return clippedPolygon;
        }

        private List<Point> ClipRectangle(Point p1, Point p2, Point p3, Point p4, Point windowMin, Point windowMax)
        {
            List<Point> clippedPoints = new List<Point>();
            List<Point> rectanglePoints = new List<Point> { p1, p2, p3, p4 };

            for (int i = 0; i < rectanglePoints.Count; i++)
            {
                int j = (i + 1) % rectanglePoints.Count;

                Point currentPoint = rectanglePoints[i];
                Point nextPoint = rectanglePoints[j];

                bool currentInside = IsInsideWindow(currentPoint, windowMin, windowMax);
                bool nextInside = IsInsideWindow(nextPoint, windowMin, windowMax);

                if (currentInside && nextInside)
                {
                    clippedPoints.Add(currentPoint);
                }
                else if (currentInside && !nextInside)
                {
                    clippedPoints.Add(currentPoint);
                    CohenSutherlandClip(ref currentPoint, ref nextPoint, windowMin, windowMax);
                    clippedPoints.Add(currentPoint);
                }
                else if (!currentInside && nextInside)
                {
                    CohenSutherlandClip(ref currentPoint, ref nextPoint, windowMin, windowMax);
                    clippedPoints.Add(currentPoint);
                }
            }

            return clippedPoints;
        }

        private void DrawLinesOnPanels(Graphics graphics, List<Point> clippedPolygon, Panel panel1, Panel panel4)
        {
            if (clippedPolygon.Count > 1)
            {
                Poligono poligono = new Poligono();
                poligono.DesenhaForma(graphics, clippedPolygon.ToArray(), panel1);
                poligono.PreencheForma(graphics, clippedPolygon.ToArray(), panel1);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                List<Point> clippedPolygon = ClipPolygon(Poligono, panel1.ClientRectangle.Location, new Point(panel1.ClientRectangle.Right, panel1.ClientRectangle.Bottom));
                DrawLinesOnPanels(graphics, clippedPolygon, panel1, panel4);
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

                    Point windowMin = new Point(0, 0);
                    Point windowMax = new Point(panel2.Width, panel2.Height);

                    List<Point> clippedRectangle = ClipRectangle(p1, p2, p3, p4, windowMin, windowMax);

                    if (clippedRectangle.Count > 2)
                    {
                        Retangulo ret = new Retangulo();
                        ret.DesenhaForma(graphics, clippedRectangle[0], clippedRectangle[2], panel2);
                        ret.PreencheForma(graphics, clippedRectangle[0], clippedRectangle[2], panel2);
                    }
                }
            }
        }
    }
}
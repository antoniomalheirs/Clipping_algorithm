using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DesenhaPrimitivas
{
    public class Desenha
    {
        protected static Pen caneta = new Pen(Color.Black, 5);
        protected static Brush caneta2 = new SolidBrush(Color.Red);

        public virtual void DesenhaForma(Graphics graphics, Point ponto1, Point ponto2)
        {
        }

        public virtual void DesenhaForma(Graphics graphics, Point[] ponto)
        {
        }

        public virtual void PreencheForma(Graphics graphics, Point ponto1, Point ponto2)
        {
        }

        public virtual void PreencheForma(Graphics graphics, Point[] ponto)
        {
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesenhaPrimitivas
{
    public class Desenha
    {
        protected static Pen caneta = new Pen(Color.Black, 2);
        protected static Pen caneta2 = new Pen(Color.Red, 2);

        public virtual void DesenhaForma(Graphics graphics, Point ponto1, Point ponto2)
        {
        }

        public virtual void DesenhaForma(Graphics graphics, Point[] ponto)
        {
        }

        public virtual void PreencheForma()
        {
        }
    }
}



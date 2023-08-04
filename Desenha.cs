using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesenhaPrimitivas
{
    internal class Desenha
    {
        static Pen caneta = new Pen(Color.Black, 2);
        static Pen caneta2 = new Pen(Color.Red, 2);

        public static void DesenhaForma()
        {
            var pen = caneta;
        }

        public static void PreencheForma()
        {
            var pen = caneta2;
        }
    }
}



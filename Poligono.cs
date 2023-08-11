using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesenhaPrimitivas
{
    internal class Poligono : Desenha
    {
        Point[] pontos;

        public override void DesenhaForma(Graphics graphics, Point[] ponto)
        {
            this.pontos = ponto;
            graphics.DrawPolygon(caneta, pontos);
        }

        public override void PreencheForma(Graphics graphics, Point[] ponto)
        {
            this.pontos = ponto;
            graphics.FillPolygon(caneta2, pontos);
        }
    }
}

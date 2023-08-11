using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesenhaPrimitivas
{
    internal class Poligono : Desenha
    {

        public override void DesenhaForma(Graphics graphics, Point[] pontos)
        {
            base.DesenhaForma(graphics, pontos);

            graphics.DrawPolygon(caneta, pontos);
        }
    }
}

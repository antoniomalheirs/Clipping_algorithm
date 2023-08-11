using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DesenhaPrimitivas
{
    internal class Retangulo : Desenha
    {
        public override void DesenhaForma(Graphics graphics, Point ponto1, Point ponto2)
        {
            int x = Math.Min(ponto1.X, ponto2.X);
            int y = Math.Min(ponto1.Y, ponto2.Y);
            int width = Math.Abs(ponto2.X - ponto1.X);
            int height = Math.Abs(ponto2.Y - ponto1.Y);
           
            graphics.DrawRectangle(caneta, x, y, width, height);
        }
    }
}

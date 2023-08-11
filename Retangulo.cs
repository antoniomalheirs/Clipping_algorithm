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
        int x;
        int y;
        int width;
        int height;

        public override void DesenhaForma(Graphics graphics, Point ponto1, Point ponto2)
        {
            this.x = Math.Min(ponto1.X, ponto2.X);
            this.y = Math.Min(ponto1.Y, ponto2.Y);
            this.width = Math.Abs(ponto2.X - ponto1.X);
            this.height = Math.Abs(ponto2.Y - ponto1.Y);
           
            graphics.DrawRectangle(caneta, x, y, width, height);
        }

        public override void PreencheForma(Graphics graphics, Point ponto1, Point ponto2)
        {
            this.x = Math.Min(ponto1.X, ponto2.X);
            this.y = Math.Min(ponto1.Y, ponto2.Y);
            this.width = Math.Abs(ponto2.X - ponto1.X);
            this.height = Math.Abs(ponto2.Y - ponto1.Y);

            graphics.FillRectangle(caneta2, x+1, y+1, width-1, height-1);
        }
    }
}

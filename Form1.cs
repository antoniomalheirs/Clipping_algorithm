using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DesenhaPrimitivas
{
    public partial class Form1 : Form
    {
        private List<Point> PontosPoligono = new List<Point>();
        private List<List<Point>> ListaRetangulos = new List<List<Point>>();

        private Point PontoInicialRetangulo;
        private Point PontoFinalRetangulo;
        private bool desenhandoRetangulo = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                DesenhaPoligono(graphics, PontosPoligono, panel4);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            PontosPoligono.Add(e.Location);
            panel4.Invalidate();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (!desenhandoRetangulo)
            {
                desenhandoRetangulo = true;
                PontoInicialRetangulo = e.Location;
            }
            else
            {
                desenhandoRetangulo = false;
                PontoFinalRetangulo = e.Location;
                List<Point> novoRetangulo = new List<Point>
                {
                    new Point(Math.Min(PontoInicialRetangulo.X, PontoFinalRetangulo.X), Math.Min(PontoInicialRetangulo.Y, PontoFinalRetangulo.Y)),
                    new Point(Math.Max(PontoInicialRetangulo.X, PontoFinalRetangulo.X), Math.Max(PontoInicialRetangulo.Y, PontoFinalRetangulo.Y))
                };
                ListaRetangulos.Add(novoRetangulo);
                panel3.Invalidate();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                foreach (var retangulo in ListaRetangulos)
                {
                    DesenhaRetangulo(graphics, retangulo, panel2);
                }
            }
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            LimparPanelComExcecao(panel4, label3, PontosPoligono);
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            LimparPanelComExcecao(panel3, label4, ListaRetangulos);
        }

        private void DesenhaPoligono(Graphics graphics, List<Point> pontos, Panel panel)
        {
            if (pontos.Count > 1)
            {
                Poligono poligono = new Poligono();
                poligono.DesenhaForma(graphics, pontos.ToArray(), panel);
                poligono.PreencheForma(graphics, pontos.ToArray(), panel);
            }
        }

        private void DesenhaRetangulo(Graphics graphics, List<Point> pontos, Panel panel)
        {
            if (pontos.Count == 2)
            {
                Point pontoSuperiorEsquerdo = pontos[0];
                Point pontoInferiorDireito = pontos[1];
                Retangulo retangulo = new Retangulo();
                retangulo.DesenhaForma(graphics, pontoSuperiorEsquerdo, pontoInferiorDireito, panel);
                retangulo.PreencheForma(graphics, pontoSuperiorEsquerdo, pontoInferiorDireito, panel);
            }
        }

        private void LimparPanelComExcecao(Panel panel, Control excecao, List<Point> pontos)
        {
            for (int i = panel.Controls.Count - 1; i >= 0; i--)
            {
                Control controle = panel.Controls[i];

                if (controle != excecao)
                {
                    panel.Controls.RemoveAt(i);
                    controle.Dispose();
                }
            }
            pontos.Clear();
            panel.Invalidate();
        }

        private void LimparPanelComExcecao(Panel panel, Control excecao, List<List<Point>> pontos)
        {
            for (int i = panel.Controls.Count - 1; i >= 0; i--)
            {
                Control controle = panel.Controls[i];

                if (controle != excecao)
                {
                    panel.Controls.RemoveAt(i);
                    controle.Dispose();
                }
            }
            pontos.Clear();
            panel.Invalidate();
        }


    }
}

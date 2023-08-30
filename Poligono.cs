using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DesenhaPrimitivas
{
    internal class Poligono : Desenha
    {
        Point[]? pontos;

        private const int CodigoEsquerda = 1;
        private const int CodigoDireita = 2;
        private const int CodigoAbaixo = 4;
        private const int CodigoAcima = 8;

        private int CalcularCodigo(Point ponto, Point janelaMin, Point janelaMax)
        {
            int codigo = 0;

            if (ponto.X < janelaMin.X)
                codigo |= CodigoEsquerda;
            else if (ponto.X > janelaMax.X)
                codigo |= CodigoDireita;

            if (ponto.Y < janelaMin.Y)
                codigo |= CodigoAcima;
            else if (ponto.Y > janelaMax.Y)
                codigo |= CodigoAbaixo;

            return codigo;
        }

        private Point Intersecao(Point ponto1, Point ponto2, Point janelaMin, Point janelaMax, int codigoPonto)
        {
            Point intersecao = new Point();

            if ((codigoPonto & CodigoAcima) != 0)
            {
                intersecao.X = ponto1.X + (ponto2.X - ponto1.X) * (janelaMin.Y - ponto1.Y) / (ponto2.Y - ponto1.Y);
                intersecao.Y = janelaMin.Y;
            }
            else if ((codigoPonto & CodigoAbaixo) != 0)
            {
                intersecao.X = ponto1.X + (ponto2.X - ponto1.X) * (janelaMax.Y - ponto1.Y) / (ponto2.Y - ponto1.Y);
                intersecao.Y = janelaMax.Y;
            }
            else if ((codigoPonto & CodigoDireita) != 0)
            {
                intersecao.Y = ponto1.Y + (ponto2.Y - ponto1.Y) * (janelaMax.X - ponto1.X) / (ponto2.X - ponto1.X);
                intersecao.X = janelaMax.X;
            }
            else if ((codigoPonto & CodigoEsquerda) != 0)
            {
                intersecao.Y = ponto1.Y + (ponto2.Y - ponto1.Y) * (janelaMin.X - ponto1.X) / (ponto2.X - ponto1.X);
                intersecao.X = janelaMin.X;
            }

            return intersecao;
        }

        private List<Point> RecortarSegmento(Point ponto1, Point ponto2, Point janelaMin, Point janelaMax)
        {
            int codigoPonto1 = CalcularCodigo(ponto1, janelaMin, janelaMax);
            int codigoPonto2 = CalcularCodigo(ponto2, janelaMin, janelaMax);

            if ((codigoPonto1 | codigoPonto2) == 0)
            {
                return new List<Point> { ponto1, ponto2 };
            }

            if ((codigoPonto1 & codigoPonto2) != 0)
            {
                return new List<Point>();
            }

            Point intersecao1, intersecao2;

            if (codigoPonto1 != 0)
            {
                intersecao1 = Intersecao(ponto1, ponto2, janelaMin, janelaMax, codigoPonto1);
                ponto1 = intersecao1;
                codigoPonto1 = CalcularCodigo(ponto1, janelaMin, janelaMax);
            }

            if (codigoPonto2 != 0)
            {
                intersecao2 = Intersecao(ponto1, ponto2, janelaMin, janelaMax, codigoPonto2);
                ponto2 = intersecao2;
                codigoPonto2 = CalcularCodigo(ponto2, janelaMin, janelaMax);
            }

            return new List<Point> { ponto1, ponto2 };
        }

        private List<Point> RecortarPoligono(List<Point> pontos, Point janelaMin, Point janelaMax)
        {
            List<Point> pontosRecortados = new List<Point>();

            for (int i = 0; i < pontos.Count; i++)
            {
                int proximoIndice = (i + 1) % pontos.Count;
                List<Point> segmentoRecortado = RecortarSegmento(pontos[i], pontos[proximoIndice], janelaMin, janelaMax);
                pontosRecortados.AddRange(segmentoRecortado);
            }

            return pontosRecortados;
        }

        public void DesenhaForma(Graphics graphics, Point[] ponto, Panel painel)
        {
            List<Point> pontosOriginais = new List<Point>(ponto);
            this.pontos = RecortarPoligono(pontosOriginais, painel.ClientRectangle.Location, new Point(painel.ClientRectangle.Right, painel.ClientRectangle.Bottom)).ToArray();
            graphics.DrawPolygon(caneta, pontos);
        }

        public void PreencheForma(Graphics graphics, Point[] ponto, Panel painel)
        {
            List<Point> pontosOriginais = new List<Point>(ponto);
            this.pontos = RecortarPoligono(pontosOriginais, painel.ClientRectangle.Location, new Point(painel.ClientRectangle.Right, painel.ClientRectangle.Bottom)).ToArray();
            graphics.FillPolygon(caneta2, pontos);
        }
    }
}

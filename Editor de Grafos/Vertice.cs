using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Editor_de_Grafos
{

   [ToolboxItem(false)]
    public class Vertice : Control
    {
        private int numero; // nº do vértice
        private bool marcado; // define se o vértice está marcado ou não
        private Color cor; // cor do vértice desmarcado
        private Color corMarcado; // cor do vértice marcado
        private GrafoBase f;

        public Vertice(int num, string rot, int x, int y, GrafoBase form)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer| ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Text = rot;
            numero = num;
            marcado = false;
            cor = Color.Black; // define a cor padrão 
            corMarcado = Color.Magenta; // define a cor do vértice marcado 
            Cursor = Cursors.Hand;
            SetXY(x, y);
            Width = x;
            Height = y;
            f = form;
        }


        public void SetXY(int x, int y) => Location = new Point(x, y);

        public int X => Location.X;

        public int Y => Location.Y;

        public void Marcar() => marcado = true;

        public void Desmarcar() => marcado = false;

        public bool Marcado => marcado;
        
        public void SetRotulo(string rot) => Text = rot;


        public void SetNum(int num) => numero = num;

        public int GetNum() => numero;


        public void SetCor(Color cor) => this.cor = cor;


        public Color GetCor() => cor;

        
        public void SetCorMarcado(Color cor) => corMarcado = cor;

        
        public Color GetCorMarcado() => corMarcado;

        public string GetRotulo() => Text;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Color cor = Marcado? GetCorMarcado() : GetCor();
            e.Graphics.FillEllipse(new SolidBrush(cor), 0, 0, 10, 10);
            e.Graphics.DrawString(Text, new Font(Font.Name, 10, FontStyle.Regular), new SolidBrush(Color.Black),0, 12);
            float tam = e.Graphics.MeasureString(Text, new Font(Font.Name, 10, FontStyle.Regular)).Width;
            SetBounds(X, Y, (int)tam, 25);
        }

        Point lastClick;
        bool drag = false; 

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            lastClick = new Point(e.X, e.Y); 
           
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if(e.Button == MouseButtons.Left) 
            {
                drag = true;    
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
                f.Refresh();
            }
        
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            drag = false;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if(!drag)
            f.clicouVertice(this);
          
        }
    }
}

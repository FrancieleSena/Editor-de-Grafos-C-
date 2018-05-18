using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Editor_de_Grafos.Domain;

namespace Editor_de_Grafos
{
    public class GrafoBase : Panel
    {
        #region [Properts]

        public static int n;
        public static List<Vertice> Vertices { get;  set; }
        private static Aresta[,] MatAdj;
        private static Vertice VMarcado;
        public static bool exibirPesos, pesosAleatorios;
        
        private int i, j, tam;
        private Vertice vi, vj;
        private Aresta a;
        
        #endregion




        Random randNum = new Random();
        public GrafoBase()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            n = 0;
            Vertices = new List<Vertice>();
            VMarcado = null;
            MatAdj = new Aresta[500, 500];
            exibirPesos = false;
            pesosAleatorios = false;
        }

        #region Get e Set

        public bool getExibirPesos()
        {
            return exibirPesos;
        }

        public bool getPesosAleatorios()
        {
            return pesosAleatorios;
        }

        public Vertice getVertice(int v)
        {
            int i;
            Vertice aux;
            for (i = 0; i < Vertices.Count; i++)
            {
                aux = Vertices[v];
                if (aux.GetNum() == v)
                    return aux;
            }
            return null;
        }

        public Vertice getVerticeMarcado()
        {
            return VMarcado;
        }

        public Aresta getAresta(int i, int j)
        {
            return MatAdj[i, j];
        }

        public int getN()
        {
            return n;
        }

        public List<Vertice> getAdjacentes(int v)
        {
            List<Vertice> lista = new List<Vertice>();
            for (int i = 0; i < n; i++)
                if (MatAdj[v, i] != null)
                    lista.Add(getVertice(i));
            return lista;
        }

        public void setExibirPesos(bool e)
        {
            exibirPesos = e;
        }

        public void setPesosAleatorios(bool e)
        {
            pesosAleatorios = e;
        }

        public void SetVerticeMarcado(Vertice v)
        {
            if (VMarcado != null)
                VMarcado.Desmarcar();
            VMarcado = v;
        }

        public void SetAresta(int i, int j, int peso)
        {
            MatAdj[i, j] = MatAdj[j, i] = new Aresta(peso, Color.DarkBlue, this);
        }

        #endregion ---------------------------------------------------------------------------------------------------

        #region Funçoes

        public void Limpar()
        {
            n = 0;
            Vertices.Clear(); // limpa a lista de vértices
            VMarcado = null; // limpa a referencia a qualquer vertice marcado
            MatAdj = new Aresta[500, 500];

            this.Controls.Clear();
            this.Refresh(); // redesenha a tela
        }

        public void AddVertice(int num, string rotulo, int x, int y)
        {
            Vertice v = new Vertice(num, rotulo, x, y, this);
            Vertices.Add(v);
            this.Controls.Add(v);
        }

        public void abrirArquivo(string path)
        {
            try
            {

                int i, j;
                string[] p;
                int ultimo, vx, vy;
                string nome;
                StreamReader s = new StreamReader(path);

                Limpar();

                n = int.Parse(s.ReadLine());
                ultimo = int.Parse(s.ReadLine());

                s.ReadLine();
                for (i = 0; i < n; i++)
                {
                    string[] data = s.ReadLine().Split(' ');
                    vx = int.Parse(data[0]); // coordenada x
                    vy = int.Parse(data[1]); // coordenada y
                    nome = data[2];
                    AddVertice(i, nome, vx, vy);
                }
                s.ReadLine();
                s.ReadLine();
                for (i = 0; i < n; i++)
                {   
                    s.ReadLine();
                }

                for (i = 1; i < n; i++)
                {
                    p = s.ReadLine().TrimEnd().Split(' ');
                    for (j = 0; j < i; j++)
                    {
                        int numero = 0;
                        bool resultado = Int32.TryParse(p[j], out numero);
                        if (resultado)
                        {
                            MatAdj[i, j] = MatAdj[j, i] = new Aresta(numero, Color.DarkBlue, this);
                        }
                        else
                        {
                            Console.WriteLine($"A conversão de '{p[j]}' Falhou.");
                        }
                    }
                }
                s.Close();
                this.Refresh();
            }
            catch (Exception eX)
            {
                MessageBox.Show("Erro ao abrir arquivo. " + eX.Message);
            }
        }

        public void SalvarArquivo(string path)
        {
            try
            {
                int i, j;
                string temp = "";
                StreamWriter r = new StreamWriter(path);
                r.WriteLine(n);
                r.WriteLine(n);
                r.WriteLine();
                for (i = 0; i < n; i++)
                {
                    r.WriteLine(getVertice(i).X+ " " +
                        getVertice(i).Y+ " " +
                        getVertice(i).Text);
                }

                r.WriteLine();

                for (i = 0; i < n; i++)
                {
                    for (j = 0; j < i; j++)
                    {
                        if (MatAdj[i, j] != null)
                            temp += "1 ";
                        else
                            temp += "0 ";
                    }
                    r.WriteLine(temp);
                    temp = "";
                }

                temp = "";

                for (i = 0; i < n; i++)
                {
                    for (j = 0; j < i; j++)
                    {
                        if (MatAdj[i, j] != null)
                            temp += MatAdj[i, j].getPeso() + " ";
                        else
                            temp += "0 ";
                    }
                    r.WriteLine(temp);
                    temp = "";
                }
                r.Close();
            }
            catch (IOException eX)
            {
                MessageBox.Show("Erro ao gravar o arquivo. " + eX.Message);
            }
        }

        public int grau(int v)
        {
            int cont = 0;
            for (int i = 0; i < n; i++)
                if (MatAdj[v, i] != null)
                    cont++;
            return cont;
        }

        public void clicouVertice(Vertice v)
        {
            if (v.Marcado)
            {
                v.Desmarcar();
                VMarcado = null;
            }
            else
            {
                v.Marcar();
                if (VMarcado != null)
                {
                    int peso;

                    if (getPesosAleatorios())
                        peso = (int)(randNum.Next(1, 100));
                    else
                        peso = 1;

                    Aresta a = new Aresta(peso, Color.DarkBlue, this); 
                    if (MatAdj[VMarcado.GetNum(), v.GetNum()] == null)
                    {
                        MatAdj[VMarcado.GetNum(), v.GetNum()] = MatAdj[v.GetNum(), VMarcado.GetNum()] = a;
                        VMarcado.Desmarcar();
                        VMarcado = v;
                    }
                    else
                    {
                        a = null;
                        v.Desmarcar();
                    }
                }
                else
                {
                    VMarcado = v;
                }
            }
            Refresh();
        }

        #endregion ----------------------------------------------------------------------------------------------------


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < i; j++)
                {
                    if (MatAdj[i, j] != null)
                    {
                        a = MatAdj[i, j];
                        vi = getVertice(i);
                        vj = getVertice(j);
                        e.Graphics.DrawLine(new Pen(a.getCor()), vi.X+ 5, vi.Y+ 5, vj.X+ 5, vj.Y+ 5);
                        if (exibirPesos)
                        {
                            tam = (int)e.Graphics.MeasureString(a.getPeso().ToString(), new Font(Font.Name, 10, FontStyle.Regular)).Width;
                            int tam1 = (tam / 2);
                            e.Graphics.FillRectangle(new SolidBrush(Color.Gray), (vi.X+ vj.X+ 10) / 2 - tam1, (vi.Y+ vj.Y) / 2 - 3.5f, tam, 18);
                            e.Graphics.DrawString(a.getPeso().ToString(), new Font(Font.Name, 10, FontStyle.Regular), new SolidBrush(Color.White), (vi.X+ vj.X+ 10) / 2 - tam1, (vi.Y+ vj.Y- 5) / 2);
                        }
                    }
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            AddVertice((n++), "V" + n, e.X, e.Y);
        }
    }
}

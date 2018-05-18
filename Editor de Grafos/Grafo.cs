using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Editor_de_Grafos
{
    public class Grafo : GrafoBase, IGrafo
    {
        private string[] cor;
        private List<int> arv;
        private bool[] visitado;

        public int AGM(int v)
        {
            IsVisited();
            arv = new List<int>(getN());
            int menorpeso, custoagm = 0, vmenori = 0, vmenorj = 0;
            arv.Add(v);
            visitado[v] = true;
            getVertice(v).SetCor(System.Drawing.Color.Red);
            while (Arvcompleta())
            {
                menorpeso = 100000;
                foreach (int ve in arv)
                {
                    for (int i = 0; i < getN(); i++)
                    {
                        if (getAresta(ve, i) != null && getAresta(ve, i).getPeso() < menorpeso && !visitado[i])
                        {
                            menorpeso = getAresta(ve, i).getPeso();
                            vmenori = ve;
                            vmenorj = i;
                        }
                        else if (grau(i) == 0)
                        {
                            visitado[i] = true;
                            getVertice(i).SetCor(System.Drawing.Color.Green);
                        }
                    }
                }
                visitado[vmenorj] = true;
                arv.Add(vmenorj);
                getVertice(vmenorj).SetCor(System.Drawing.Color.Red);
                custoagm += getAresta(vmenori, vmenorj).getPeso();
                getAresta(vmenori, vmenorj).setCor(System.Drawing.Color.Red);
                Refresh();
                
            }
            return custoagm;
        }

        public bool Arvcompleta()
        {
            for (int i = 0; i < visitado.Length; i++)
            {
                if (!visitado[i])
                {
                    return true;
                }
            }
            return false;
        }

        public void CaminhoMinimo(int i, int j)
        {
            Console.WriteLine(Dijkstras(i, j));
        }



        public List<String> Dijkstras(int start, int finish)
        {
            var previous = new Dictionary<int, int>();
            var distances = new Dictionary<int, int>();
            var nodes = new List<Vertice>();

            List<String> path = null;

            foreach (var vertex in GrafoBase.Vertices)
            {
                if (vertex.GetNum() == start)
                {
                    distances[vertex.GetNum()] = 0;
                }
                else
                {
                    distances[vertex.GetNum()] = int.MaxValue;
                }

                nodes.Add(vertex);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x.GetNum()] - distances[y.GetNum()]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest.GetNum() == finish)
                {
                    path = new List<String>();
                    while (previous.ContainsKey(smallest.GetNum()))
                    {
                        path.Add(smallest.GetRotulo());
                        smallest =  this.getVertice(previous[smallest.GetNum()]);
                    }

                    break;
                }

                if (distances[smallest.GetNum()] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in GrafoBase.Vertices)
                {
                    var alt = distances[smallest.GetNum()] + neighbor.GetNum();
                    if (alt < distances[neighbor.GetNum()])
                    {
                        distances[neighbor.GetNum()] = alt;
                        previous[neighbor.GetNum()] = smallest.GetNum();
                    }
                }
            }

            return path;
        }
    



    public void CompletarGrafo()
        {
            for (int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++)
                {
                    if (i != j)
                    {
                        SetAresta(i, j, 1);
                    }
                }
            }
            Refresh();
        }

        public bool IsEuleriano()
        {
            int i;
            for (i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0)
                    return false;
            }
            return getN() != 0;
        }

        public bool IsUnicursal()
        {
            int i, count=0;
            for (i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0)
                    count++;
            }
            return !(count > 2);
        }

        public void Largura(int v)
        {
            Queue<Int32> f = new Queue<int>();
            f.Enqueue(v);
            visitado[v] = true;
            while (f.Count > 0)
            {
                v = f.Dequeue();
                for (int i = 0; i < getN(); i++)
                {
                    if (getAresta(v, i) != null && !visitado[i])
                    {
                        getAresta(v, i).setCor(System.Drawing.Color.Red);
                        f.Enqueue(i);
                        visitado[i] = true;
                    }
                }
            }
            Refresh();
        }
    
      
        public int NumeroCromatico()
        {
            int v = 0;
            for (int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++)
                {
                    //acha o vértice de maior grau
                    if (grau(i) > grau(v))
                    {
                        v = i;
                    }
                }
            }
            Queue<Int32> f = new Queue<int>();
            f.Enqueue(v);
            visitado[v] = true;
            getVertice(v).SetCor(System.Drawing.Color.FromName(cor[0]));
            Refresh();
            while (f.Count > 0)
            {
                v = f.Dequeue();
                for (int i = 0; i < getN(); i++)
                {
                    if (getAresta(v, i) != null && !visitado[i])
                    {
                        f.Enqueue(i);
                        visitado[i] = true;
                        getAresta(v, i).setCor(System.Drawing.Color.Green);
                        getVertice(i).SetCor(System.Drawing.Color.FromName(cor[0]));
                        Refresh();
                        for (int x = 1; x < cor.Length; x++)
                        {
                            for (int j = 0; j < getN(); j++)
                            {
                                if (getAresta(i, j) != null && getVertice(i).GetCor() == getVertice(j).GetCor())
                                {
                                    getVertice(i).SetCor(System.Drawing.Color.FromName(cor[x]));
                                    Refresh();
                                }
                                else if (grau(j) == 0)
                                {
                                    getVertice(j).SetCor(System.Drawing.Color.FromName(cor[0]));
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(1000);
                    }

                }

            }
            return contcor();
        }

        //numero cromatico
        public int contcor()
        {
            int aux = 0;
            //for de cor
            for (int x = 0; x < cor.Length; x++)
            {
                //for pecorre cada vertice para cada cor
                for (int i = 0; i < getN(); i++)
                {
                    if (getVertice(i).GetCor() == System.Drawing.Color.FromName(cor[x]))
                    {
                        aux++;
                        //encontrou uma cor ja passa para outra
                        break;
                    }
                }
            }
            return aux;

        }

        public bool Arvore()
        {
            Largura(0);
            for (int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++)
                {
                    if (getAresta(i, j) != null)
                    {
                        if (getAresta(i, j).getCor() == System.Drawing.Color.Black)
                        {
                            MessageBox.Show("Não Árvore!");
                            return false;
                        }
                    }
                }
            }
            MessageBox.Show("Árvore!");
            return true;
        }

        public String ParesOrdenados()
        {
            string mensagem = "{";

            for (int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++)
                {
                    if (getAresta(i, j) != null)
                    {
                        mensagem += "(" + i + ", " + j + ")";
                    }
                }
            }

            mensagem += "}";
            return mensagem;
        }

        public void IsVisited()
        {
            visitado = new bool[getN()];
            cor = new string[6] { "Black", "Yellow", "Pink", "Red", "Cyan", "Azure"};
        }


        public void Profundidade(int v)
        {
            visitado[v] = true;
            for (int i = 0; i < getN(); i++)
            {
                if (getAresta(v,i) != null && !visitado[i])
                {
                    getAresta(v, i).setCor(System.Drawing.Color.Red);
                    Profundidade(i);
                }
            }
            Refresh();
        }

        public void LimpaGrafo()
        {
            for(int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++)
                {
                    if (getAresta(i,j) != null)
                    {
                        getAresta(i,j).setCor(System.Drawing.Color.Black);
                    }
                }
            }
            Refresh();
        }
    }
}

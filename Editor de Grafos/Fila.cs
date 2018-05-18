using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Editor_de_Grafos
{

    public class Objeto<T> : IComparable<Objeto<T>>
    {
        List<int> lst = new List<int>();
        T _obj;
        int chave;

        public T Obj { get => _obj; set => _obj = value; }
        public int Prioridade { get => chave; set => chave = value; }

        public int CompareTo(Objeto<T> obj)
        {
            if(obj.Prioridade > Prioridade)
                return 1;
            if (obj.Prioridade < Prioridade)
                return -1;

            return 0;
        }
    }

    public  class FilaPrioridade<T>
    {
         Stack<Objeto<T>> nodes = new Stack<Objeto<T>>();

         public void Enfileira(int prioridade, T key)
        {
            Objeto<T> ob = new Objeto<T> { Obj = key, Prioridade = prioridade };
            this.nodes.Push(ob);
            var temp =  this.nodes.ToList(); this.nodes.Clear();
            temp.Sort();
            temp.ForEach(x => nodes.Push(x));
        }

        public  Objeto<T> Desenfileira()
        {
            return this.nodes.Pop();
        }

        public bool IsVazia() => nodes.Count == 0;
    }

}

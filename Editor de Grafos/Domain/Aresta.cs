using System.Drawing;
using System.Windows.Forms;

namespace Editor_de_Grafos.Domain
{
    public class Aresta
    {   
        public Aresta(Panel ed, Color cor, int peso)
        {
            Ed = ed;
            Cor = cor;
            Peso = peso;
        }

        private int _peso;
        private Color _cor;
        Panel _ed;
        
        public Panel Ed { get => _ed; set => _ed = value; }
        public Color Cor { get => _cor; set => _cor = value; }
        public int Peso { get => _peso; set => _peso = value; }
    }
}

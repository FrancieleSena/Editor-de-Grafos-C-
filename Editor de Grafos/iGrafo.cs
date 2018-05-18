using System;
using System.Collections.Generic;
using System.Text;

namespace Editor_de_Grafos
{
    interface IGrafo
    {
        bool IsEuleriano();
        bool IsUnicursal();
        string ParesOrdenados();
        void CompletarGrafo();
        void Profundidade(int v);
        void Largura(int v);
        int AGM(int v);
        void CaminhoMinimo(int i, int j);
        int NumeroCromatico();
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Editor_de_Grafos
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();            
        }

        #region Botoes de Algoritmo do Menu
        private void BtParesOrd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pares Ordnados:" + g.ParesOrdenados());
        }

        private void BtGrafoEuleriano_Click(object sender, EventArgs e)
        {
            if(g.IsEuleriano())
                MessageBox.Show("O grafo e Euleriano!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("O grafo não e Euleriano!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtGrafoUnicursal_Click(object sender, EventArgs e)
        {
            if (g.IsUnicursal())
                MessageBox.Show("O grafo é Unicursal!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("O grafo não e Unicursal!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtBuscaProfundidade_Click(object sender, EventArgs e)
        {
            g.IsVisited();
            g.Profundidade(0);
        }

        #endregion --------------------------------------------------------------------------------------------------

        #region botoes restantes do menu

        private void BtNovo_Click(object sender, EventArgs e)
        {
            g.Limpar();
        }

        private void BtAbrir_Click(object sender, EventArgs e)
        {
            if(OPFile.ShowDialog() == DialogResult.OK)
            {
                g.abrirArquivo(OPFile.FileName);
                g.Refresh();
            }
        }

        private void BtSalvar_Click(object sender, EventArgs e)
        {
            if(SVFile.ShowDialog() == DialogResult.OK)
            {
                g.SalvarArquivo(SVFile.FileName);
            }
        }

        private void BtSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtPeso_Click(object sender, EventArgs e)
        {
            if(BtPeso.Checked)
            {
                BtPeso.Checked = false;
                g.setExibirPesos(false);

            }
            else
            {
                BtPeso.Checked = true;
                g.setExibirPesos(true);
            }
            g.Refresh();
        }

        private void BtPesoAleatorio_Click(object sender, EventArgs e)
        {
            if(BtPesoAleatorio.Checked)
            {
                BtPesoAleatorio.Checked = false;
                g.setPesosAleatorios(false);
            }
            else
            {
                BtPesoAleatorio.Checked = true;
                g.setPesosAleatorios(true);
            }
        }

        private void BtSobre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Editor de Grafos - 2018/1\n\nDesenvolvido por:\nNatan Macedo de Magalhaes\nVirgilio Borges de Oliveira\n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.", "Sobre o Editor de Grafos...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion --------------------------------------------------------------------------------------------------

        private void larguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.IsVisited();
            g.Largura(0);
        }

        private void algoritmosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.IsVisited();
            g.LimpaGrafo();
        }

        private void caminhoMínimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.CaminhoMinimo(1, 3);
        }

        private void completarGrafoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.CompletarGrafo();
        }

        private void arvoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Arvore();
        }

        private void númroCromáticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.NumeroCromatico();
        }

        private void aGMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.AGM(0);
        }

        //public void ShowMyDialogBox()
        //{
        //    Form testDialog = new Form();

        //    // Show testDialog as a modal dialog and determine if DialogResult = OK.
        //    if (testDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        // Read the contents of testDialog's TextBox.
        //        this.txtResult.Text = testDialog.TextBox1.Text;
        //    }
        //    else
        //    {
        //        this.txtResult.Text = "Cancelled";
        //    }
        //    testDialog.Dispose();
        //}

    }
}

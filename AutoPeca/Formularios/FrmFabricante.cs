using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class FrmFabricante : Form
    {
        private VO.Fabricante fab;
        private List<VO.Fabricante> lista;
        private BE.FabricanteBE be;

        public FrmFabricante()
        {
            InitializeComponent();
            InicializarVeiculos();
            liberarEdicao(false);
            carregar();
        }

        private void InicializarVeiculos()
        {
            fab = new VO.Fabricante();
            if (DAO.DAO.listaFabricante == null)
            {
                DAO.DAO.listaFabricante = new List<VO.Fabricante>();
            }
            lista = DAO.DAO.listaFabricante;
        }

        private void InteractToObject()
        {

            fab.codigo = int.Parse(txtcod.Text);
            fab.nome = txtnome.Text;
            fab.descricao = txtdesc.Text;
        }
        private void interfaceToObject()
        {

            fab.codigo = int.Parse(txtcod.Text);
            fab.nome = txtnome.Text;
            fab.descricao = txtdesc.Text;
            //vo.fabricante = cmbFabricante.SelectedItem.ToString();

        }

        private void Limpar()
        {
            txtnome.Text = "";
            txtdesc.Text = "";
            txtcod.Text = "";
            
            //cmbFabricante.SelectedIndex = -1;
        }

        private void carregar()
        {
            be = new BE.FabricanteBE(this.fab);
            lstfabricante.DataSource = null;

            lstfabricante.DataSource = lista;
            lstfabricante.SelectedIndex = -1;
            lstfabricante.ValueMember = "codigo";
            lstfabricante.DisplayMember = "nome";
            lstfabricante.Refresh();
        }

        private void btnlimpar2_Click(object sender, EventArgs e)
        {
            Limpar();
            liberarEdicao(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                fab = new VO.Fabricante();
                InteractToObject();
                be = new BE.FabricanteBE(this.fab);
                lista.Add(fab);               
                be.incluir();
                Limpar();
                carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro No Aplicativo");

            }
        }

        private void btnselecionar_Click(object sender, EventArgs e)
        {
            fab = ((VO.Fabricante)lstfabricante.Items[lstfabricante.SelectedIndex]);
            txtcod.Text = fab.codigo.ToString();
            txtnome.Text = fab.nome.ToString();
            txtdesc.Text = fab.descricao.ToString();
            liberarEdicao(true);
        }
        private void liberarEdicao(bool habilita)
        {
            btnGravar.Enabled = !habilita;
            btnEditar.Enabled = habilita;
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            interfaceToObject();
            be = new BE.FabricanteBE(this.fab);
            be.alterar();
            carregar();
            Limpar();
            liberarEdicao(false);

        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            lista.RemoveAt(lstfabricante.SelectedIndex);
            //carregar();

            be = new BE.FabricanteBE(this.fab);
            fab = (VO.Fabricante)lstfabricante.SelectedItem;
            be.remover(fab.codigo);
            carregar();
        }
    }

}


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
    public partial class FrmClientes : Form
    {
        private VO.Clientes vo;
        private List<VO.Clientes> lista;
        private BE.ClientesBE be;
        public FrmClientes()
        {
            InitializeComponent();
            InicializarClientes();
            liberarEdicao(false);
            carregar();
            //carregarFabricante();
        }

        /*private void carregarFabricante()
        {
            cmbFabricante.DataSource = null;
            cmbFabricante.DataSource = DAO.DAO.listaFabricante;
            cmbFabricante.ValueMember = "codigo";
            cmbFabricante.DisplayMember = "nome";
            cmbFabricante.Refresh();
        }*/

        private void InicializarClientes()
        {
            vo = new VO.Clientes();
        }

        private void interfaceToObject()
        {

            vo.nome = txtnome.Text;
            vo.CPF = txtcpf.Text;
            vo.endereco = txtendereco.Text;
            vo.numero = txtnumero.Text;
            vo.cidade = txtcidade.Text;
            vo.estado = txtestado.Text;
            vo.pais = txtpais.Text;
            //vo.fabricante = cmbFabricante.SelectedItem.ToString();

        }
        private void objecttoInterface()
        {
            txtcod.Text = vo.codigo.ToString();
            txtnome.Text = vo.nome.ToString();
            txtcpf.Text = vo.CPF.ToString();
            txtendereco.Text = vo.endereco.ToString();
            txtnumero.Text = vo.numero.ToString();
            txtcidade.Text = vo.cidade.ToString();
            txtestado.Text = vo.estado.ToString();
            txtpais.Text = vo.pais.ToString();
            //cmbFabricante.SelectedItem = vo.fabricante.ToString();
        }
        private void Limpar()
        {
            txtnome.Text = "";
            txtcpf.Text = "";
            txtendereco.Text = "";
            txtnumero.Text = "";
            txtcidade.Text = "";
            txtestado.Text = "";
            txtpais.Text = "";
            //cmbFabricante.SelectedIndex = -1;
        }
        private void carregar()
        {
            be = new BE.ClientesBE(this.vo);
            lstClientes.DataSource = null;
            lstClientes.DataSource = be.listar();
            lstClientes.SelectedIndex = -1;
            lstClientes.ValueMember = "codigo";
            lstClientes.DisplayMember = "descricao";
            lstClientes.Refresh();

        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
            liberarEdicao(false);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                vo = new VO.Clientes();
                interfaceToObject();
                be = new BE.ClientesBE(this.vo);
                be.incluir();
                carregar();
                Limpar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro no Aplicativo");
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            be = new BE.ClientesBE(this.vo);
            vo = be.carregar(int.Parse(lstClientes.SelectedValue.ToString()));
            objecttoInterface();
            liberarEdicao(true);
        }

        private void liberarEdicao(bool habilita)
        {
            btnGravar.Enabled = !habilita;
            btnEditar.Enabled = habilita;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            interfaceToObject();
            be = new BE.ClientesBE(this.vo);
            be.alterar();
            carregar();
            Limpar();
            liberarEdicao(false);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            be = new BE.ClientesBE(this.vo);
            vo = (VO.Clientes)lstClientes.SelectedItem;
            be.remover(vo.codigo);          
            carregar();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {

        }
    }
}

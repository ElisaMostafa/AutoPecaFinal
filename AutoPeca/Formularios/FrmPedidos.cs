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
    public partial class FrmPedidos : Form
    {
        private VO.Pedidos vo;
        private BE.PedidosBE be;

        public FrmPedidos()
        {
            InitializeComponent();
            InicializarVeiculos();
            liberarEdicao(false);
            carregar();
            carregarCliente();
            carregarPeça();
        }

        private void carregarCliente()
        {
            BE.ClientesBE vo = new BE.ClientesBE(new VO.Clientes());
            cmbcodcliente.DataSource = null;
            cmbcodcliente.DataSource = vo.listar();
            cmbcodcliente.ValueMember = "codigo";
            cmbcodcliente.DisplayMember = "nome";
            cmbcodcliente.Refresh();
        }

        private void carregarPeça()
        {
            BE.PecasBE vo = new BE.PecasBE(new VO.Pecas());
            cmbcodpeca.DataSource = null;
            cmbcodpeca.DataSource = vo.listar();
            cmbcodpeca.ValueMember = "codigo";
            cmbcodpeca.DisplayMember = "nome";
            cmbcodpeca.Refresh();
        }

        private void InicializarVeiculos()
        {
            vo = new VO.Pedidos();
        }

        private void InteractToObject()
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                vo.codigo = int.Parse(txtCodigo.Text);
            }
            vo.dataPedido =  DateTime.Parse(txtdata.Text);
            vo.pecas = (VO.Pecas)cmbcodpeca.SelectedItem;
            vo.clientes = (VO.Clientes)cmbcodcliente.SelectedItem;
        }

        private void objecttoInterface()
        {
            txtCodigo.Text = vo.codigo.ToString();
            txtdata.Text = vo.dataPedido.ToString();
            int index = 0;
            foreach (VO.Pecas item in cmbcodpeca.Items)
            {
                if (item.codigo.Equals(vo.pecas.codigo))
                {
                    cmbcodpeca.SelectedIndex = index;
                    return;
                }
                index++;
            }
            foreach (VO.Clientes item in cmbcodcliente.Items)
            {
                if (item.codigo.Equals(vo.clientes.codigo))
                {
                    cmbcodcliente.SelectedIndex = index;
                    return;
                }
                index++;
            }
        }

        private void limpar()
        {
            txtCodigo.Text = "";
            txtdata.Text = "";
            cmbcodcliente.SelectedIndex = -1;
            cmbcodpeca.SelectedIndex = -1;
        }

        private void carregar()
        {
            be = new BE.PedidosBE(this.vo);
            lstPedidos.DataSource = null;
            lstPedidos.DataSource = be.listar();
            lstPedidos.ValueMember = "codigo";
            lstPedidos.DisplayMember = "codigo";
            lstPedidos.Refresh();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
            liberarEdicao(false);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                vo = new VO.Pedidos();
                InteractToObject();
                be = new BE.PedidosBE(this.vo);
                be.incluir();
                carregar();
                limpar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro No Aplicativo");

            }
        }

        private void btnselecionar_Click(object sender, EventArgs e)
        {
            be = new BE.PedidosBE(this.vo);
            vo = be.carregar(int.Parse(lstPedidos.SelectedValue.ToString()));
            objecttoInterface();
            liberarEdicao(true);
        }

        private void liberarEdicao(bool habilita)
        {
            btnGravar.Enabled = !habilita;
            btnEditar.Enabled = habilita;
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            InteractToObject();
            be = new BE.PedidosBE(this.vo);
            be.alterar();
            carregar();
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            be = new BE.PedidosBE(this.vo);
            vo = (VO.Pedidos)lstPedidos.SelectedItem;
            be.remover(vo.codigo);
            carregar();
        }
    }
}

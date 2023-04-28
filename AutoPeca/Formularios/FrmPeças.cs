using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class FrmPeças : Form
    {
        private VO.Pecas vo;
        private List<VO.Pecas> lista;
        private BE.PecasBE be;
        public FrmPeças()
        {
            InitializeComponent();
            InicializarVeiculos();
            liberarEdicao(false);
            carregar();
            carregarVeiculo();
        }

       private void carregarVeiculo()
        {
            BE.VeiculoBE vo = new BE.VeiculoBE(new VO.Veiculo());
            cmbidveiculo.DataSource = null;
            cmbidveiculo.DataSource = vo.listar();
            cmbidveiculo.ValueMember = "codigo";
            cmbidveiculo.DisplayMember = "nome";
            cmbidveiculo.Refresh();
        }

        private void InicializarVeiculos()
        {
            vo = new VO.Pecas();
        }

        private void interfaceToObject()
        {

            if (!string.IsNullOrEmpty(txtcod.Text))
            {
                vo.codigo = int.Parse(txtcod.Text);
            }
            vo.descricao = txtdescricao.Text;
            vo.codigoBarras = txtcodbarras.Text;
            vo.veiculo = (VO.Veiculo)cmbidveiculo.SelectedItem;

        }
        private void objecttoInterface()
        {
            txtdescricao.Text = vo.descricao.ToString();
            txtcodbarras.Text = vo.codigoBarras.ToString();
            txtcod.Text = vo.codigo.ToString();
            int index = 0;
            foreach (VO.Fabricante item in cmbidveiculo.Items)
            {
                if (item.codigo.Equals(vo.veiculo.codigo))
                {
                    cmbidveiculo.SelectedIndex = index;
                    return;
                }
                index++;
            }

           // cmbidveiculo.SelectedItem = vo.veiculo.ToString();
        }
        private void Limpar()
        {
            txtdescricao.Text = "";
            txtcodbarras.Text = "";
            txtcod.Text = "";
            cmbidveiculo.SelectedIndex = -1;


            
        }
        private void carregar()
        {
            be = new BE.PecasBE(this.vo);
            lstPecas.DataSource = null;
            lstPecas.DataSource = be.listar();
            //lstPecas.SelectedIndex = -1;
            lstPecas.ValueMember = "codigo";
            lstPecas.DisplayMember = "descricao";
            lstPecas.Refresh();

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
                vo = new VO.Pecas();
                interfaceToObject();
                be = new BE.PecasBE(this.vo);
                be.incluir();
                carregar();
                Limpar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro no Sistema");
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            be = new BE.PecasBE(this.vo);
            vo = be.carregar(int.Parse(lstPecas.SelectedValue.ToString()));
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
            be = new BE.PecasBE(this.vo);
            be.alterar();
            carregar();
            Limpar();
            liberarEdicao(false);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            be = new BE.PecasBE(this.vo);
            vo = (VO.Pecas)lstPecas.SelectedItem;
            be.remover(vo.codigo);
            carregar();
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.BE
{
    public class PedidosBE : BaseBE
    {
        private VO.Pedidos vo;
        private DAO.PedidosDAO dao;

        public PedidosBE(VO.Pedidos vo)
        {
            this.vo = vo;
        }

        public void incluir()
        {
            if (this.vo.dataPedido == null)
            {
                throw new Exception("Data do pedido obrigatório!");
            }
            dao = new DAO.PedidosDAO(this.vo);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.PedidosDAO(this.vo);
            dao.alterar();
        }
        public VO.Pedidos carregar(int id)
        {
            dao = new DAO.PedidosDAO(this.vo);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.PedidosDAO(this.vo);
            dao.remover(id);
        }

        public List<VO.Pedidos> listar()
        {
            dao = new DAO.PedidosDAO(this.vo);
            return dao.listar();
        }
    }
}

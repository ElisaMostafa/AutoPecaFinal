using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.BE
{
    public class ClientesBE : BaseBE
    {
        private VO.Clientes vo;
        private DAO.ClientesDAO dao;

        public ClientesBE(VO.Clientes vo)
        {
            this.vo = vo;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.vo.nome))
            {
                throw new Exception("Nome do veículo obrigatório!");
            }

            dao = new DAO.ClientesDAO(this.vo);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.ClientesDAO(this.vo);
            dao.alterar();
        }
        public VO.Clientes carregar(int id)
        {
            dao = new DAO.ClientesDAO(this.vo);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.ClientesDAO(this.vo);
            dao.remover(id);
        }

        public List<VO.Clientes> listar()
        {
            dao = new DAO.ClientesDAO(this.vo);
            return dao.listar();
        }
    }
}

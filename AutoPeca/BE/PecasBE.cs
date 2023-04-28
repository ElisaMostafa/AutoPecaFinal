using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.BE
{
    public class PecasBE : BaseBE
    {
        private VO.Pecas vo;
        private DAO.PecasDao dao;

        public PecasBE(VO.Pecas vo)
        {
            this.vo = vo;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.vo.descricao))
            {
                throw new Exception("Descriçao obrigatória!");
            }

            dao = new DAO.PecasDao(this.vo);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.PecasDao(this.vo);
            dao.alterar();
        }
        public VO.Pecas carregar(int id)
        {
            dao = new DAO.PecasDao(this.vo);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.PecasDao(this.vo);
            dao.remover(id);
        }

        public List<VO.Pecas> listar()
        {
            dao = new DAO.PecasDao(this.vo);
            return dao.listar();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class PecasDao : BaseDAO
    {
        private VO.Pecas vo;
        public PecasDao(VO.Pecas vo)
        {

            if (DAO.listaPecas == null)
            {
                DAO.listaPecas = new List<VO.Pecas>();
            }
            this.vo = vo;
        }

        public void incluir()
        {
            try
            {
                string sql = "insert into tb_pecas (NM_DESCRICAO,NM_CODIGOBARRAS,ID_VEICULO) " +
                    "values (@descricao,@codigobarras,@veiculo)";
                db.AddParameter("@descricao", vo.descricao, ParameterDirection.Input);
                db.AddParameter("@codigobarras", vo.codigoBarras, ParameterDirection.Input);
                db.AddParameter("@veiculo", vo.veiculo.codigo, ParameterDirection.Input);

                db.Execute(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
        public void alterar()
        {
            try
            {
                string sql = "update tb_pecas set " +
                    "NM_DESCRICAO = @descricao," +
                    "NM_CODIGOBARRAS = @codigobarras," +
                    "where ID = @id";
                db.AddParameter("@descricao", vo.descricao, ParameterDirection.Input);
                db.AddParameter("@codigobarras", vo.codigoBarras, ParameterDirection.Input);
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
        public void remover(int id)
        {
            try
            {
                string sql = $"delete from tb_pecas where ID = @id";
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        public VO.Pecas carregar(int id)
        {
            string sql = $"SELECT id,nm_descricao,nm_codigobarras from tb_pecas where id=@id";
            db.AddParameter("@id", id, ParameterDirection.Input);
            try
            {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadPecas(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Pecas LoadPecas(DbDataReader dr)
        {
            vo = new VO.Pecas();
            vo.codigo = Convert.ToInt32(dr["ID"]);
            vo.descricao = dr["nm_descricao"] != DBNull.Value ? dr["nm_descricao"].ToString() : "";
            vo.codigoBarras = dr["nm_codigobarras"] != DBNull.Value ? dr["nm_codigobarras"].ToString() : "";

            vo.veiculo = new VO.Veiculo();
            vo.veiculo.codigo = dr["id_veiculo"] != DBNull.Value ? int.Parse(dr["id_veiculo"].ToString()) : 0;
            return vo;
        }

        public List<VO.Pecas> listar()
        {
            try
            {
                string sql = "SELECT * FROM tb_pecas;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Pecas>();

                    while (dr.Read())
                    {
                        vo = LoadPecas(dr);
                        objResultado.Add(vo);
                    }
                    return objResultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class ClientesDAO : BaseDAO
    {
        private VO.Clientes vo;
        public ClientesDAO(VO.Clientes vo)
        {

            if (DAO.listaClientes == null)
            {
                DAO.listaClientes = new List<VO.Clientes>();
            }
            this.vo = vo;
        }

        public void incluir()
        {
            try
            {
                string sql = "insert into tb_clientes (NM_NOME,NM_CPF,NM_ENDERECO,NM_NUMERO,NM_CIDADE,NM_ESTADO,NM_PAIS) " +
                    "values (@nome,@cpf,@endereco,@numero,@cidade,@estado,@pais)";
                db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                db.AddParameter("@cpf", vo.CPF, ParameterDirection.Input);
                db.AddParameter("@endereco", vo.endereco, ParameterDirection.Input);
                db.AddParameter("@numero", vo.numero, ParameterDirection.Input);
                db.AddParameter("@cidade", vo.cidade, ParameterDirection.Input);
                db.AddParameter("@estado", vo.estado, ParameterDirection.Input);
                db.AddParameter("@pais", vo.pais, ParameterDirection.Input);

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
                string sql = "update tb_clientes set " +
                    "NM_NOME = @nome," +
                    "NM_CPF = @CPF ," +
                    "NM_ENDERECO = @endereco ," +
                    "NM_NUMERO = @numero ," +
                    "NM_CIDADE = @cidade ," +
                    "NM_ESTADO = @estado ," +
                    "NM_PAIS = @pais " +
                    "where ID = @id";
                db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                db.AddParameter("@CPF", vo.CPF, ParameterDirection.Input);
                db.AddParameter("@endereco", vo.endereco, ParameterDirection.Input);
                db.AddParameter("@numero", vo.numero, ParameterDirection.Input);
                db.AddParameter("@cidade", vo.cidade, ParameterDirection.Input);
                db.AddParameter("@estado", vo.estado, ParameterDirection.Input);
                db.AddParameter("@pais", vo.pais, ParameterDirection.Input);
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
                string sql = $"delete from tb_clientes where ID = @id";
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        public VO.Clientes carregar(int id)
        {
            string sql = $"SELECT id,nm_nome,nm_CPF,nm_endereco,nm_numero,nm_cidade,nm_estado,nm_pais from tb_clientes where id=@id";
            db.AddParameter("@id", id, ParameterDirection.Input);
            try
            {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadClientes(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Clientes LoadClientes(DbDataReader dr)
        {
            vo = new VO.Clientes();
            vo.codigo = Convert.ToInt32(dr["ID"]);
            vo.nome = dr["nm_nome"] != DBNull.Value ? dr["nm_nome"].ToString() : "";
            vo.CPF = dr["nm_CPF"] != DBNull.Value ? dr["nm_CPF"].ToString() : "";
            vo.endereco = dr["nm_endereco"] != DBNull.Value ? dr["nm_endereco"].ToString() : "";
            vo.numero = dr["nm_numero"] != DBNull.Value ? dr["nm_numero"].ToString() : "";
            vo.cidade = dr["nm_cidade"] != DBNull.Value ? dr["nm_cidade"].ToString() : "";
            vo.estado = dr["nm_estado"] != DBNull.Value ? dr["nm_estado"].ToString() : "";
            vo.pais = dr["nm_pais"] != DBNull.Value ? dr["nm_pais"].ToString() : "";
            return vo;
        }

        public List<VO.Clientes> listar()
        {
            try
            {
                string sql = "SELECT * FROM tb_clientes;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Clientes>();

                    while (dr.Read())
                    {
                        vo = LoadClientes(dr);
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

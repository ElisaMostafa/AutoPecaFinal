using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class PedidosDAO : BaseDAO
    {
        private VO.Pedidos vo;
        public PedidosDAO(VO.Pedidos vo)
        {

            if (DAO.listaPedidos == null)
            {
                DAO.listaPedidos = new List<VO.Pedidos>();
            }
            this.vo = vo;
        }

        public void incluir()
        {
            try
            {
                string sql = "insert into tb_pedidos (NM_DATAPEDIDO, ID_CLIENTES, ID_PECAS) " +
                    "values (@dataPedido,@clientes,@pecas)";
                db.AddParameter("@dataPedido", vo.dataPedido, ParameterDirection.Input);
                db.AddParameter("@clientes", vo.clientes.codigo, ParameterDirection.Input);
                db.AddParameter("@pecas", vo.pecas.codigo, ParameterDirection.Input);

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
                string sql = "update tb_pedidos set " +
                    "NM_DATAPEDIDO = @dataPedido," +
                    "where ID = @id";
                db.AddParameter("@dataPedido", vo.dataPedido, ParameterDirection.Input);
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
                string sql = $"delete from tb_pedidos where ID = @id";
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        public VO.Pedidos carregar(int id)
        {

            string sql = $"SELECT id,nm_datapedido,id_clientes,id_pecas from tb_pedidos where id=@id";
            db.AddParameter("@id", id, ParameterDirection.Input);
            try
            {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadPedidos(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Pedidos LoadPedidos(DbDataReader dr)
        {
            vo = new VO.Pedidos();
            vo.codigo = Convert.ToInt32(dr["ID"]);
            vo.dataPedido = Convert.ToDateTime(dr["NM_DATAPEDIDO"]);

            vo.clientes = new VO.Clientes();
            vo.clientes.codigo = dr["id_clientes"] != DBNull.Value ? int.Parse(dr["id_clientes"].ToString()) : 0;
            vo.pecas = new VO.Pecas();
            vo.pecas.codigo = dr["id_pecas"] != DBNull.Value ? int.Parse(dr["id_pecas"].ToString()) : 0;
            return vo;
        }

        public List<VO.Pedidos> listar()
        {
            try
            {
                string sql = "SELECT * FROM tb_pedidos;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Pedidos>();

                    while (dr.Read())
                    {
                        vo = LoadPedidos(dr);
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

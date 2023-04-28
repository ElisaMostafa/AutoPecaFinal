using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class VeiculoDAO : BaseDAO
    {
        private VO.Veiculo vo;
        public VeiculoDAO(VO.Veiculo vo) {

            if (DAO.listaVeiculo == null)
            {
                DAO.listaVeiculo = new List<VO.Veiculo>();
            }
            this.vo = vo;
        }
       
        public void incluir()
        {
            try
            {
                string sql = "insert into tb_veiculo (NM_MODELO,VL_ANO,NM_MOTOR,ID_FABRICANTE) " +
                    "values (@Mod,@ano,@mot,@fab)";
                db.AddParameter("@Mod", vo.modelo, ParameterDirection.Input);
                db.AddParameter("@ano", vo.ano, ParameterDirection.Input);
                db.AddParameter("@mot", vo.potencia, ParameterDirection.Input);
                db.AddParameter("@fab", vo.fabricante.codigo, ParameterDirection.Input);
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
                string sql = "update tb_veiculo set " +
                    "NM_MODELO = @modelo," +
                    "VL_ANO = @ano ,"+
                     "ID_FABRICANTE = @fab ," +
                    "NM_MOTOR = @potencia " +
                    "where ID = @id";
                db.AddParameter("@modelo", vo.modelo, ParameterDirection.Input);
                db.AddParameter("@ano", vo.ano, ParameterDirection.Input);
                db.AddParameter("@potencia", vo.potencia, ParameterDirection.Input);
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);
                db.AddParameter("@fab", vo.fabricante.codigo, ParameterDirection.Input);
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
                string sql = $"delete from tb_veiculo where ID = @id";
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
       
        public VO.Veiculo carregar(int id)        {
            string sql = $"SELECT id,nm_modelo,vl_ano,NM_MOTOR,ID_FABRICANTE from tb_veiculo where id=@id";
            db.AddParameter("@id", id, ParameterDirection.Input);
            try {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadVeiculos(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Veiculo LoadVeiculos(DbDataReader dr)
        {
            vo = new VO.Veiculo();
            vo.codigo = Convert.ToInt32(dr["ID"]);
            vo.modelo = dr["nm_modelo"] != DBNull.Value ? dr["nm_modelo"].ToString() : "";
            vo.ano = dr["vl_ano"] != DBNull.Value ? int.Parse(dr["vl_ano"].ToString()) : 0;
            vo.potencia = dr["NM_MOTOR"] != DBNull.Value ? dr["NM_MOTOR"].ToString() : "";
            vo.fabricante = new VO.Fabricante();
            vo.fabricante.codigo = dr["id_fabricante"] != DBNull.Value ? int.Parse(dr["id_fabricante"].ToString()) : 0;

            return vo;
        }

        public List<VO.Veiculo> listar()
        {
            try
            {
                string sql = "SELECT * FROM tb_veiculo;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Veiculo>();

                    while (dr.Read())
                    {
                        vo  = LoadVeiculos(dr);
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

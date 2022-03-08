using DAO.ConexionDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Cocktail
    {
        private readonly IConexionBD conexiondb;

        public Cocktail(IConexionBD conexiondb)
        {
            this.conexiondb = conexiondb;
        }

        public int CreaListCocktail(object cocktailXML)
        {
            int totalRegistrosCreados = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexiondb.GetDbConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_cocktail_allSave", cnx))
                    {
                        cnx.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@REGISTROSXML", SqlDbType.Xml).Value = cocktailXML;

                        // OutPut
                        SqlParameter outputIdParam = cmd.Parameters.Add("@RTNVALUE", SqlDbType.Int);
                        outputIdParam.Direction = ParameterDirection.Output;

                        //Devuelve el numero de filas afectadas.
                        cmd.ExecuteNonQuery();

                        totalRegistrosCreados = (int)outputIdParam.Value;

                        cnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }

            return totalRegistrosCreados;
        }
    }
}

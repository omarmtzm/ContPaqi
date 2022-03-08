using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.ConexionDB;
using Entity;

namespace DAO
{
    public class Usuarios: ICrud<Entity.Usuario>
    {
        private readonly IConexionBD  conexiondb;

        public Usuarios(IConexionBD conexiondb)
        {
            this.conexiondb = conexiondb;
        }

        public int Actualizar(Usuario usuario)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexiondb.GetDbConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_seUsuario_Update", cnx))
                    {
                        cnx.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@NOMBREUSUARIO", SqlDbType.VarChar).Value = usuario.NombreUsuario;
                        cmd.Parameters.Add("@CODIGOUSUARIO", SqlDbType.VarChar).Value = usuario.CodigoUsuario;
                        cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = usuario.Password;
                        cmd.Parameters.Add("@ESTATUS", SqlDbType.Bit).Value = usuario.Estatus;
                        cmd.Parameters.Add("@CORREOELECTRONICO", SqlDbType.VarChar).Value = usuario.CorreoElectronico;
                        cmd.Parameters.Add("@USUARIOID", SqlDbType.VarChar).Value = usuario.UsuarioID;

                        // Return
                        SqlParameter returnParameter = cmd.Parameters.Add("@Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        int resp = cmd.ExecuteNonQuery();

                        resultado = (int)returnParameter.Value;

                        cnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }

            return resultado;
        }

        public int Crear(Usuario usuario)
        {
            int usuarioID = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexiondb.GetDbConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_seUsuario_Insert", cnx))
                    {
                        cnx.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@NOMBREUSUARIO", SqlDbType.VarChar).Value = usuario.NombreUsuario;
                        cmd.Parameters.Add("@CODIGOUSUARIO", SqlDbType.VarChar).Value = usuario.CodigoUsuario;
                        cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = usuario.Password;                
                        cmd.Parameters.Add("@ESTATUS", SqlDbType.Bit).Value = usuario.Estatus;
                        cmd.Parameters.Add("@CORREOELECTRONICO", SqlDbType.VarChar).Value = usuario.CorreoElectronico;

                        // OutPut
                        SqlParameter outputIdParam = cmd.Parameters.Add("@USUARIOID", SqlDbType.Int);
                        outputIdParam.Direction = ParameterDirection.Output;

                        //Devuelve el numero de filas afectadas.
                        cmd.ExecuteNonQuery();

                        usuarioID = (int)outputIdParam.Value;

                        cnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }

            return usuarioID;
        }

        public int Eliminar(int id)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexiondb.GetDbConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_seUsuario_Delete", cnx))
                    {
                        cnx.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIOID", SqlDbType.Int).Value = id;

                        // Return
                        SqlParameter returnParameter = cmd.Parameters.Add("@Return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        int resp = cmd.ExecuteNonQuery();

                        resultado = (int)returnParameter.Value;

                        cnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }

            return resultado;
        }

        public List<Usuario> GetAll()
        {
            List<Entity.Usuario> listUsuarios = new List<Entity.Usuario>();
            SqlDataReader dr = null;

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexiondb.GetDbConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_seUsuario_All", cnx))
                    {
                        cnx.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Entity.Usuario usuario = new Entity.Usuario();

                                usuario.UsuarioID = Convert.ToInt32(dr["UsuarioID"]);
                                usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                                usuario.CodigoUsuario = dr["CodigoUsuario"].ToString();
                                usuario.Password = dr["Password"].ToString();
                                usuario.Estatus = Convert.ToBoolean(dr["Estatus"]);
                                usuario.CorreoElectronico = dr["CorreoElectronico"].ToString();
                                usuario.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"]);
                                usuario.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);

                                listUsuarios.Add(usuario);
                            }
                        }
                        cnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listUsuarios;
        }

        public Usuario GetByID(int id)
        {
            Entity.Usuario usuario = new Entity.Usuario();
            SqlDataReader dr = null;

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexiondb.GetDbConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_seUsuario_GetUsuarioID", cnx))
                    {
                        cnx.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIOID", SqlDbType.Int).Value = id;

                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                usuario.UsuarioID = Convert.ToInt32(dr["UsuarioID"]);
                                usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                                usuario.CodigoUsuario = dr["CodigoUsuario"].ToString();
                                usuario.Password = dr["Password"].ToString();
                                usuario.Estatus = Convert.ToBoolean(dr["Estatus"]);
                                usuario.CorreoElectronico = dr["CorreoElectronico"].ToString();
                                usuario.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"]);
                                usuario.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                                
                            }
                        }

                        cnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new( ex.Message);
            }

            return usuario;
        }
    }
}

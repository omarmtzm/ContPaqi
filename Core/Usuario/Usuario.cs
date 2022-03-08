using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Usuario
{
    public class Usuario : IUsuario
    {          
        private readonly DAO.ConexionDB.IConexionBD conexion;

        public Usuario(DAO.ConexionDB.IConexionBD conexion)
        {
            this.conexion = conexion;
        }

        public ReturnObject Actualizar(Entity.Usuario usuario)
        {
            int resultado = 0;
            Entity.ReturnObject retorno = new Entity.ReturnObject();

            retorno.result = false;
            retorno.message = "Ocurrio un error en la clase Core Usuario Metodo Actualizar al iniciarla.";
            retorno.detail = null;

            try
            {
                DAO.Usuarios dataUsuario = new DAO.Usuarios(conexion);

                // Es esta parte encripta la contraseña este es de un solo camino
                usuario.Password = Seguridad.EncryptGetSHA256(usuario.Password);

                /*Valida los campos obligatorios*/
                if (usuario.NombreUsuario == "")
                {
                    retorno.message = "El campo Nombre de Usuario es un camapo valido";
                    return retorno;
                }
                if (usuario.CodigoUsuario == "")
                {
                    retorno.message = "El campo Codigo de Usuario es un camapo valido";
                    return retorno;
                }
                if (usuario.Password == "")
                {
                    retorno.message = "El campo Password es un camapo valido";
                    return retorno;
                }
                if (usuario.CorreoElectronico == "")
                {
                    retorno.message = "El campo Correo Electronico es un camapo valido";
                    return retorno;
                }

                resultado = dataUsuario.Actualizar(usuario);

                if (resultado > 0)
                {
                    if (resultado == 1004)
                    {
                        retorno.result = true;
                        retorno.message = "Se Actualizo con exito el ID " + usuario.UsuarioID.ToString() + " del Usuario" + usuario.NombreUsuario;
                        retorno.detail = resultado;
                    }

                    if (resultado == 1003)
                    {
                        retorno.result = false;
                        retorno.message = "Ocurrio un error al actualizar los datos.";
                        retorno.detail = null;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = false;
                retorno.message = ex.Message;
                retorno.detail = null;
            }

            return retorno;
        }

        public ReturnObject Crear(Entity.DTO.CreaUsuario creUsuario)
        {
            int usuarioID = 0;
            Entity.Usuario usuario = new Entity.Usuario();
            Entity.ReturnObject retorno = new Entity.ReturnObject();

            retorno.result = false;
            retorno.message = "Ocurrio un error en la clase Core Usuario Metodo Crear al iniciarla.";
            retorno.detail = "";

            try
            {
                DAO.Usuarios dataUsuario = new DAO.Usuarios(conexion);

                usuario.NombreUsuario = creUsuario.NombreUsuario;
                usuario.CodigoUsuario = creUsuario.CodigoUsuario;
                // Es esta parte encripta la contraseña este es de un solo camino
                usuario.Password = Seguridad.EncryptGetSHA256(creUsuario.Password); 
                usuario.Estatus =  creUsuario.Estatus;
                usuario.CorreoElectronico = creUsuario.CorreoElectronico;

                /*Valida los campos obligatorios*/
                if (usuario.NombreUsuario == "")
                {
                    retorno.message = "El campo Nombre de Usuario es un camapo valido";
                    return retorno;
                }
                if (usuario.CodigoUsuario == "")
                {
                    retorno.message = "El campo Codigo de Usuario es un camapo valido";
                    return retorno;
                }
                if (usuario.Password == "")
                {
                    retorno.message = "El campo Password es un camapo valido";
                    return retorno;
                }                
                if (usuario.CorreoElectronico == "")
                {
                    retorno.message = "El campo Correo Electronico es un camapo valido";
                    return retorno;
                }

                //Guarda el registro
                usuarioID = dataUsuario.Crear(usuario);

                if (usuarioID != 0)
                {
                    retorno.result = true;
                    retorno.message = "Se creo con exito el ID " + usuarioID.ToString() + " del autobus" + usuario.UsuarioID;
                    retorno.detail = usuarioID;
                }
                else
                {
                    retorno.result = false;
                    retorno.message = "No se encontraron registros en la base de datos.";
                    retorno.detail = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = false;
                retorno.message = ex.Message;
                retorno.detail = 0;
            }

            return retorno;
        }

        public ReturnObject Eliminar(int id)
        {
            int resultado = 0;

            Entity.ReturnObject retorno = new Entity.ReturnObject();

            retorno.result = false;
            retorno.message = "Ocurrio un error en la clase Core Usuario Metodo Eliminar Usuario al iniciarla.";
            retorno.detail = null;

            try
            {
                DAO.Usuarios dataUsuarios = new DAO.Usuarios(conexion);
                resultado = dataUsuarios.Eliminar(id);

                if (id > 0)
                {
                    if (resultado == 1006)
                    {
                        retorno.result = true;
                        retorno.message = "Se elimino con exito el Usuario con ID " + id.ToString();
                        retorno.detail = id;
                    }

                    if (resultado == 1005)
                    {
                        retorno.result = false;
                        retorno.message = "Ocurrio un error al eliminar los datos.";
                        retorno.detail = null;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = false;
                retorno.message = ex.Message;
                retorno.detail = null;
            }

            return retorno;
        }

        public ReturnObject GetAll()
        {
            List<Entity.Usuario> listaUsuarios = new List<Entity.Usuario>();
            ReturnObject rtnObj = new ReturnObject();
            rtnObj.result = false;
            rtnObj.message = "Ocurrio un error en la clase Core Usuario Metodo GetAll al iniciarla.";
            rtnObj.detail = null;

            try
            {
                DAO.Usuarios usuario = new DAO.Usuarios(conexion);
                listaUsuarios = usuario.GetAll();           

                rtnObj.result = true;
                rtnObj.message = "";
                rtnObj.detail = listaUsuarios;

                if (listaUsuarios.Count==0)
                {
                    rtnObj.message = "No se encontrar registros en la base de datos";
                }
                
            }
            catch (Exception ex)
            {
                rtnObj.result = false;
                rtnObj.message = ex.Message;
                rtnObj.detail = null;
            }            

            return rtnObj;
        }

        public ReturnObject GetByID(int id)
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();

            retorno.result = false;
            retorno.message = "Ocurrio un error en la clase Core Usuario Metodo GetByID al iniciarla.";
            retorno.detail = null;

            try
            {
                DAO.Usuarios dataUsuario = new DAO.Usuarios(conexion);
                Entity.Usuario usuario = new Entity.Usuario();

                usuario = dataUsuario.GetByID(id);

                if (usuario != null)
                {
                    retorno.result = true;
                    retorno.message = "";
                    retorno.detail = usuario;
                }
                else
                {
                    retorno.result = false;
                    retorno.message = "No se encontraron registros en la base de datos.";
                    retorno.detail = null;
                }
            }
            catch (Exception ex)
            {
                retorno.result = false;
                retorno.message = ex.Message;
                retorno.detail = null;
            }

            return retorno;
        }
    }
}

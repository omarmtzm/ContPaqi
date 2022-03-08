using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContPaqiUIWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Core.Usuario.IUsuario usuario;
        private readonly DAO.ConexionDB.IConexionBD conexionBD;

        public UsuarioController(Core.Usuario.IUsuario usuario, DAO.ConexionDB.IConexionBD conexionBD)
        {
            this.usuario = usuario;
            this.conexionBD = conexionBD;
        }


        // GET: UsuarioController
        public ActionResult Index()
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();

            retorno.result = false;
            retorno.message = "Ocurrio un error en el Controlador de Usuario Controller Index.";
            retorno.detail = "";

            retorno = usuario.GetAll();
            return View(retorno);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();
            retorno.result = true;
            retorno.message = "";
            retorno.detail = "";

            return View(retorno);
        }

        // POST: UsuarioController/Create
        [HttpPost]   
        public JsonResult Create(IFormCollection collection)
        {
            Entity.ReturnObject rtnObj = new Entity.ReturnObject();
            rtnObj.result = false;
            rtnObj.message = "Ocurrio un error en el controler Usuario y accion Create";
            rtnObj.detail = null;

            Entity.DTO.CreaUsuario creaUsuario = new Entity.DTO.CreaUsuario();

            creaUsuario.NombreUsuario = collection["nombreUsuario"].ToString();
            creaUsuario.CodigoUsuario = collection["codigoUsuario"].ToString();
            creaUsuario.Password = collection["password"].ToString();
            creaUsuario.Estatus = Convert.ToBoolean(collection["estatus"]);
            creaUsuario.CorreoElectronico = collection["correoElectronico"].ToString();
            
            rtnObj = usuario.Crear(creaUsuario);

            return Json(rtnObj);
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();
            retorno.result = false;
            retorno.message = "Ocurrio un error en el Controlador de Usuario Controller Editar.";
            retorno.detail = null;

            retorno = usuario.GetByID(id);                 

            return View(retorno);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]     
        public JsonResult Edit(IFormCollection collection)
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();
            retorno.result = false;
            retorno.message = "Ocurrio un error en el Controlador de Usuario de la accion Edit.";
            retorno.detail = "";

            Entity.Usuario usuario = new Entity.Usuario();

            usuario.UsuarioID = Convert.ToInt32(collection["UsuarioID"]);
            usuario.NombreUsuario = collection["nombreUsuario"].ToString();
            usuario.CodigoUsuario = collection["CodigoUsuario"].ToString();
            usuario.Password = collection["Password"].ToString();
            usuario.Estatus = Convert.ToBoolean(collection["estatus"]);
            usuario.CorreoElectronico = collection["correoElectronico"].ToString();

            retorno = this.usuario.Actualizar(usuario);          

            return Json(retorno);
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();
            retorno.result = false;
            retorno.message = "Ocurrio un error en el Controlador de Usuario controller Elimina.";
            retorno.detail = null;

            retorno = usuario.GetByID(id);
   
            return View(retorno);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]        
        public ActionResult Delete(IFormCollection collection)
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();
            retorno.result = false;
            retorno.message = "Ocurrio un error en el Controlador de Usuario de la accion Eliminar.";
            retorno.detail = null;

            int usuarioID = Convert.ToInt32(collection["usuarioID"]);

            retorno = usuario.Eliminar(usuarioID);           

            return Json(retorno);
           
        }
    }
}

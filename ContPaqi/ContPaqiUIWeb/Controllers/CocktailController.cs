using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContPaqiUIWeb.Controllers
{
    public class CocktailController : Controller
    {
        private readonly DAO.ConexionDB.IConexionBD conexionBD;

        public CocktailController(DAO.ConexionDB.IConexionBD conexionBD)
        {
            this.conexionBD = conexionBD;
        }
        public IActionResult Index()
        {
            List<Entity.DTO.TablaCocktail> tablaCocktails = new List<Entity.DTO.TablaCocktail>();
            Entity.ReturnObject retorno = new Entity.ReturnObject();
            retorno.result = true;
            retorno.message = "";
            retorno.detail = tablaCocktails;

            //Core.Cocktail cocktail = new Core.Cocktail(conexionBD); 
            //retorno = cocktail.GetByID("");
            
            return View(retorno);
        }

        public PartialViewResult ParcialReporteCocktail()
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();
            retorno.result = false;
            retorno.message = "Ocurrio un error en la clase Cocktail Metodo ParcialReporteCocktail al iniciarla.";
            retorno.detail = "";
            List<Entity.DTO.TablaCocktail> tablaCocktails = new List<Entity.DTO.TablaCocktail>();

            string cocktail = Request.Form["cocktail"].ToString();            
            
            try
            {
                Core.Cocktail coreCocktail = new Core.Cocktail(conexionBD);
                retorno =coreCocktail.GetByID(cocktail);                
            }
            catch (Exception ex)
            {
                retorno.result = false;
                retorno.message = ex.Message;
                retorno.detail = tablaCocktails;
            }

            return PartialView("_DetalleCocktail",retorno);
        }
    }
}

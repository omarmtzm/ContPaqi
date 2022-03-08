using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.ConexionDB;
using Entity;

namespace Core
{
    public class Cocktail
    {

        private readonly IConexionBD conexiondb;

        public Cocktail(IConexionBD conexiondb)
        {
            this.conexiondb = conexiondb;
        }

        public ReturnObject GetByID(string strDrink)
        {
            Entity.ReturnObject retorno = new Entity.ReturnObject();
            retorno.result = false;
            retorno.message = "Ocurrio un error en la clase Core Cocktail Metodo GetByID al iniciarla.";
            retorno.detail = null;
            List<Entity.cocktail> listCocktails = new List<Entity.cocktail>();
            int totalRegistrosCreados = 0;

            try
            {
                DAO.WebApiConsumer.ApiConsumer ApiConsumerCocktail = new DAO.WebApiConsumer.ApiConsumer();
                
                listCocktails = ApiConsumerCocktail.GetByID(strDrink);

                if (listCocktails.Count > 0)
                {
                    DataTable dtCocktails = CopyCocktailsToDatatable(listCocktails);

                    object filexml = MakeXMLofTable(dtCocktails);

                    DAO.Cocktail cocktail = new DAO.Cocktail(conexiondb);

                    totalRegistrosCreados = cocktail.CreaListCocktail(filexml);
                }

                List<Entity.DTO.TablaCocktail> tablaCocktails = listCocktails.
                                                                Select(x => new Entity.DTO.TablaCocktail()
                                                                {
                                                                    idDrink = x.idDrink,
                                                                    strDrink = x.strDrink,
                                                                    strCategory = x.strCategory,
                                                                    strAlcoholic = x.strAlcoholic,
                                                                    strGlass = x.strGlass,
                                                                    strDrinkThumb = x.strDrinkThumb,
                                                                    dateModified = x.dateModified
                                                                }).ToList();

                retorno.result = true;
                retorno.message = "Se crearon " + totalRegistrosCreados.ToString() + " registros y se consumio los servios REST API para cargar la pantalla" ;
                retorno.detail = tablaCocktails;

                if (listCocktails.Count == 0)
                {
                    retorno.message = "No se encontrar registros en la base de datos";
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

        public DataTable CopyCocktailsToDatatable(List<Entity.cocktail> listCocktails)
        {            
            DataTable dtCocktails = new DataTable("dtCtl");

            try
            {
                //Add Columns to DataTable
                dtCocktails.Columns.Add("idDrink");
                dtCocktails.Columns.Add("strDrink");
                dtCocktails.Columns.Add("strDrinkAlternate");
                dtCocktails.Columns.Add("strTags");
                dtCocktails.Columns.Add("strVideo");
                dtCocktails.Columns.Add("strCategory");
                dtCocktails.Columns.Add("strIBA");
                dtCocktails.Columns.Add("strAlcoholic");
                dtCocktails.Columns.Add("strGlass");
                dtCocktails.Columns.Add("strInstructions");
                dtCocktails.Columns.Add("strInstructionsES");
                dtCocktails.Columns.Add("strInstructionsDE");
                dtCocktails.Columns.Add("strInstructionsFR");
                dtCocktails.Columns.Add("strInstructionsIT");
                dtCocktails.Columns.Add("strInstructionsZH_HANS");
                dtCocktails.Columns.Add("strInstructionsZH_HANT");
                dtCocktails.Columns.Add("strDrinkThumb");
                dtCocktails.Columns.Add("strIngredient1");
                dtCocktails.Columns.Add("strIngredient2");
                dtCocktails.Columns.Add("strIngredient3");
                dtCocktails.Columns.Add("strIngredient4");
                dtCocktails.Columns.Add("strIngredient5");
                dtCocktails.Columns.Add("strIngredient6");
                dtCocktails.Columns.Add("strIngredient7");
                dtCocktails.Columns.Add("strIngredient8");
                dtCocktails.Columns.Add("strIngredient9");
                dtCocktails.Columns.Add("strIngredient10");
                dtCocktails.Columns.Add("strIngredient11");
                dtCocktails.Columns.Add("strIngredient12");
                dtCocktails.Columns.Add("strIngredient13");
                dtCocktails.Columns.Add("strIngredient14");
                dtCocktails.Columns.Add("strIngredient15");
                dtCocktails.Columns.Add("strMeasure1");
                dtCocktails.Columns.Add("strMeasure2");
                dtCocktails.Columns.Add("strMeasure3");
                dtCocktails.Columns.Add("strMeasure4");
                dtCocktails.Columns.Add("strMeasure5");
                dtCocktails.Columns.Add("strMeasure6");
                dtCocktails.Columns.Add("strMeasure7");
                dtCocktails.Columns.Add("strMeasure8");
                dtCocktails.Columns.Add("strMeasure9");
                dtCocktails.Columns.Add("strMeasure10");
                dtCocktails.Columns.Add("strMeasure11");
                dtCocktails.Columns.Add("strMeasure12");
                dtCocktails.Columns.Add("strMeasure13");
                dtCocktails.Columns.Add("strMeasure14");
                dtCocktails.Columns.Add("strMeasure15");
                dtCocktails.Columns.Add("strImageSource");
                dtCocktails.Columns.Add("strImageAttribution");
                dtCocktails.Columns.Add("strCreativeCommonsConfirmed");
                dtCocktails.Columns.Add("dateModified");

                //Loop through the Object and copy rows.
                int rowNum = 0;
                foreach (var item in listCocktails)
                {
                    dtCocktails.Rows.Add();
                    dtCocktails.Rows[rowNum][0] = item.idDrink;
                    dtCocktails.Rows[rowNum][1] = ConvertStringNullToEmpty(item.strDrink);
                    dtCocktails.Rows[rowNum][2] = ConvertStringNullToEmpty(item.strDrinkAlternate);
                    dtCocktails.Rows[rowNum][3] = ConvertStringNullToEmpty(item.strTags);
                    dtCocktails.Rows[rowNum][4] = ConvertStringNullToEmpty(item.strVideo);
                    dtCocktails.Rows[rowNum][5] = ConvertStringNullToEmpty(item.strCategory);
                    dtCocktails.Rows[rowNum][6] = ConvertStringNullToEmpty(item.strIBA);
                    dtCocktails.Rows[rowNum][7] = ConvertStringNullToEmpty(item.strAlcoholic);
                    dtCocktails.Rows[rowNum][8] = ConvertStringNullToEmpty(item.strGlass);
                    dtCocktails.Rows[rowNum][9] = ConvertStringNullToEmpty(item.strInstructions);
                    dtCocktails.Rows[rowNum][10] = ConvertStringNullToEmpty(item.strInstructionsES);
                    dtCocktails.Rows[rowNum][11] = ConvertStringNullToEmpty(item.strInstructionsDE);
                    dtCocktails.Rows[rowNum][12] = ConvertStringNullToEmpty(item.strInstructionsFR);
                    dtCocktails.Rows[rowNum][13] = ConvertStringNullToEmpty(item.strInstructionsIT);
                    dtCocktails.Rows[rowNum][14] = ConvertStringNullToEmpty(item.strInstructionsZH_HANS);
                    dtCocktails.Rows[rowNum][15] = ConvertStringNullToEmpty(item.strInstructionsZH_HANT);
                    dtCocktails.Rows[rowNum][16] = ConvertStringNullToEmpty(item.strDrinkThumb);
                    dtCocktails.Rows[rowNum][17] = ConvertStringNullToEmpty(item.strIngredient1);
                    dtCocktails.Rows[rowNum][18] = ConvertStringNullToEmpty(item.strIngredient2);
                    dtCocktails.Rows[rowNum][19] = ConvertStringNullToEmpty(item.strIngredient3);
                    dtCocktails.Rows[rowNum][20] = ConvertStringNullToEmpty(item.strIngredient4);
                    dtCocktails.Rows[rowNum][21] = ConvertStringNullToEmpty(item.strIngredient5);
                    dtCocktails.Rows[rowNum][22] = ConvertStringNullToEmpty(item.strIngredient6);
                    dtCocktails.Rows[rowNum][23] = ConvertStringNullToEmpty(item.strIngredient7);
                    dtCocktails.Rows[rowNum][24] = ConvertStringNullToEmpty(item.strIngredient8);
                    dtCocktails.Rows[rowNum][25] = ConvertStringNullToEmpty(item.strIngredient9);
                    dtCocktails.Rows[rowNum][26] = ConvertStringNullToEmpty(item.strIngredient10);
                    dtCocktails.Rows[rowNum][27] = ConvertStringNullToEmpty(item.strIngredient11);
                    dtCocktails.Rows[rowNum][28] = ConvertStringNullToEmpty(item.strIngredient12);
                    dtCocktails.Rows[rowNum][29] = ConvertStringNullToEmpty(item.strIngredient13);
                    dtCocktails.Rows[rowNum][30] = ConvertStringNullToEmpty(item.strIngredient14);
                    dtCocktails.Rows[rowNum][31] = ConvertStringNullToEmpty(item.strIngredient15);
                    dtCocktails.Rows[rowNum][32] = ConvertStringNullToEmpty(item.strMeasure1);
                    dtCocktails.Rows[rowNum][33] = ConvertStringNullToEmpty(item.strMeasure2);
                    dtCocktails.Rows[rowNum][34] = ConvertStringNullToEmpty(item.strMeasure3);
                    dtCocktails.Rows[rowNum][35] = ConvertStringNullToEmpty(item.strMeasure4);
                    dtCocktails.Rows[rowNum][36] = ConvertStringNullToEmpty(item.strMeasure5);
                    dtCocktails.Rows[rowNum][37] = ConvertStringNullToEmpty(item.strMeasure6);
                    dtCocktails.Rows[rowNum][38] = ConvertStringNullToEmpty(item.strMeasure7);
                    dtCocktails.Rows[rowNum][39] = ConvertStringNullToEmpty(item.strMeasure8);
                    dtCocktails.Rows[rowNum][40] = ConvertStringNullToEmpty(item.strMeasure9);
                    dtCocktails.Rows[rowNum][41] = ConvertStringNullToEmpty(item.strMeasure10);
                    dtCocktails.Rows[rowNum][42] = ConvertStringNullToEmpty(item.strMeasure11);
                    dtCocktails.Rows[rowNum][43] = ConvertStringNullToEmpty(item.strMeasure12);
                    dtCocktails.Rows[rowNum][44] = ConvertStringNullToEmpty(item.strMeasure13);
                    dtCocktails.Rows[rowNum][45] = ConvertStringNullToEmpty(item.strMeasure14);
                    dtCocktails.Rows[rowNum][46] = ConvertStringNullToEmpty(item.strMeasure15);
                    dtCocktails.Rows[rowNum][47] = ConvertStringNullToEmpty(item.strImageSource);
                    dtCocktails.Rows[rowNum][48] = ConvertStringNullToEmpty(item.strImageAttribution);
                    dtCocktails.Rows[rowNum][49] = ConvertStringNullToEmpty(item.strCreativeCommonsConfirmed);
                    dtCocktails.Rows[rowNum][50] = ConvertStringNullToEmpty(item.dateModified);

                    rowNum++;
                }

            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }

            return dtCocktails;
        }

        public object MakeXMLofTable(DataTable dt)
        {
            string doctoXML = "";
            try
            {
                using (TextWriter writer = new StringWriter())
                {
                    DataTable Items = new DataTable();
                    if(dt !=null)
                    {
                        Items = dt;
                    }

                    Items.WriteXml(writer);
                    var xml = writer.ToString();

                    doctoXML = xml;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return doctoXML;
        }

        public string ConvertStringNullToEmpty(string valor)
        {
            string retorno = "";

            if (valor==null) 
            { 
                retorno = string.Empty; 
            }
            else
            {
                retorno = valor;
            }

            return retorno;
        }

        public DateTime ConvertDateTimeNullToEmpty(Nullable<DateTime> valor)
        {
            DateTime retorno = DateTime.MinValue;

            if (valor == null)
            {
                retorno = DateTime.MinValue;
            }
            else
            {
                retorno = Convert.ToDateTime(valor);
            }

            return retorno;
        }
    }
}

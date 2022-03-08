using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.WebApiConsumer
{
    public class ApiConsumer
    {
        public List<Entity.cocktail> GetByID(string strDrink)
        {            
            Entity.Drinks drinks = new Entity.Drinks();
            //Hosted web API REST Service base url
            string Baseurl = "https://www.thecocktaildb.com/api/json/v1/1/";
            string path = "search.php?s=" + strDrink;           

            try
            {
                using (HttpClient client =  new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method HTTP GET
                    HttpResponseMessage response = client.GetAsync(path).Result;
                    if (response.IsSuccessStatusCode)
                    {                        
                        var readTask = response.Content.ReadAsStringAsync().Result;
                        drinks = JsonConvert.DeserializeObject<Entity.Drinks>(readTask);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }

            return drinks.drinks;
        }
    }
}

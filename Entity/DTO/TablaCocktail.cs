using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public class TablaCocktail
    {
        public int idDrink { get; set; }
        public string strDrink { get; set; }
        public string strCategory { get; set; }
        public string strAlcoholic { get; set; }
        public string strGlass { get; set; } 
        public string strDrinkThumb { get; set; }        
        public string dateModified { get; set; }
    }
}

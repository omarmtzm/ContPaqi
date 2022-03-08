using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public class CreaUsuario
    {
        [Required]
        [StringLength(60, ErrorMessage = "El tamaño del campo Nombre de Usuario no puede se mayor a 60 carateres.")]
        public string NombreUsuario { get; set; }
        [Required]
        [StringLength(10)]
        public string CodigoUsuario { get; set; }
        [Required]
        [StringLength(200)]
        public string Password { get; set; }
        [Required]       
        public Boolean Estatus { get; set; }
        [Required]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }
       
    }
}

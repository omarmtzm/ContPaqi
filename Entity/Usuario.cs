using System;

namespace Entity
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string Password { get; set; }
        public Boolean Estatus { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

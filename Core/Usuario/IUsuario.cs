using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Core.Usuario
{
    public interface IUsuario
    {
        public ReturnObject GetAll();

        public ReturnObject GetByID(int id);

        public ReturnObject Crear(Entity.DTO.CreaUsuario usuario);

        public ReturnObject Actualizar(Entity.Usuario usuario);

        public ReturnObject Eliminar(int id);
    }
}

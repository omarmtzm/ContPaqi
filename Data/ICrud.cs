using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface ICrud<T>
    {
         public List<T> GetAll();

         public T GetByID(int id);

         public int Crear(T t);

         public int Actualizar(T t);

         public int Eliminar(int id);
    }
}

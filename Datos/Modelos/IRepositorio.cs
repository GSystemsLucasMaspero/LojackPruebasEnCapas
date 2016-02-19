using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelos
{
    public interface IRepositorio<T> where T : class
    {
        void Agregar(T entidad);
        void Modificar(T entidad, int id);
        T ObtenerPorID(int id);
        IEnumerable<T> ObtenerTodos();
    }
}

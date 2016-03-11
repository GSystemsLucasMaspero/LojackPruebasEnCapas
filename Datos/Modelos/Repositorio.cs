using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.DBContexto;

namespace Datos.Modelos
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private Lojack_PruebaEntities DB = new Lojack_PruebaEntities();

        protected Lojack_PruebaEntities DBContext
        {
            get { return DB; }
        }

        public virtual void Agregar(T entidad)
        {
            DB.Set<T>().Add(entidad);
            DB.SaveChanges();
        }

        public virtual void Modificar(T entidad, int id)
        {
            DB.SaveChanges();
        }

        public T ObtenerPorID(int id)
        {
            return DB.Set<T>().Find(id);
        }

        public IQueryable<T> ObtenerTodos()
        {
            return DB.Set<T>();
        }
    }
}

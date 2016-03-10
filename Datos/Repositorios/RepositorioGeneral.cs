using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class RepositorioGeneral
    {
        private Lojack_PruebaEntities DB = new Lojack_PruebaEntities();

        public DateTime ObtenerDateTimeServer()
        {
            return DB.Database.SqlQuery<DateTime>("SELECT GETDATE()").AsEnumerable().First();
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return DB.Usuarios.ToList();
        }

        public Usuario ObtenerUsuarioPorNombre(String userLogin)
        {
            return DB.Usuarios.FirstOrDefault(u => u.userLogin == userLogin);
        }

    }
}

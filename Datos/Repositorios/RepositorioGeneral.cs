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
        private Lojack_Prueba DB = new Lojack_Prueba();

        public DateTime ObtenerDateTimeServer()
        {
            return DB.Database.SqlQuery<DateTime>("SELECT GETDATE()").AsEnumerable().First();
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return DB.Usuarios.ToList();
        }
    }
}

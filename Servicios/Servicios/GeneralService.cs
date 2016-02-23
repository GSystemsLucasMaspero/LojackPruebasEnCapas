using Datos.DBContexto;
using Datos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class GeneralService
    {
        private RepositorioGeneral repositorio = new RepositorioGeneral();

        public DateTime GetServerDateTime()
        {
            return repositorio.ObtenerDateTimeServer();
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return repositorio.ObtenerUsuarios();
        }
    }
}

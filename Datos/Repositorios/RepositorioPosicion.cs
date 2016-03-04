using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class RepositorioPosicion
    {
        private Lojack_PruebaEntities DB = new Lojack_PruebaEntities();

        public bool TieneEntidadDeID(int idEntidad)
        {
            return DB.Posicions.Where(p => p.idEntidad == idEntidad).FirstOrDefault() != null;
        }

        public Posicion ObtenerUltimaPosicion(int idEntidad)
        {
            return DB.Posicions.Where(posicion => posicion.idEntidad == idEntidad).OrderBy(posicion => posicion.fechaPosicion).First();
        }

        public IEnumerable<Posicion> ObtenerPosiciones(int idEntidad)
        {
            return DB.Posicions.Where(posicion => posicion.idEntidad == idEntidad);
        }

        public IEnumerable<Int32> ObtenerIdEntidadConPosicion()
        {
            IEnumerable<Int32> data = DB.Posicions.Select(p => p.idEntidad).Distinct().ToList();
            return data;
        }

    }
}

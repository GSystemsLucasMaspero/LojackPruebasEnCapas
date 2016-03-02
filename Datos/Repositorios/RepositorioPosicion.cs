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

        // Obtener primera posición encontrada.
        public Posicion ObtenerPosicion(int idEntidad)
        {
            return DB.Posicions.First(posicion => posicion.idEntidad == idEntidad);
        }

        public Posicion ObtenerUltimaPosicion(int idEntidad)
        {
            Posicion ultima = null;
            foreach(Posicion p in DB.Posicions.Where(posicion => posicion.idEntidad == idEntidad)) {
                if (ultima == null || DateTime.Compare(ultima.fechaPosicion, p.fechaPosicion) < 0)
                    ultima = p;
            }
            return ultima;
        }

        public IEnumerable<Posicion> ObtenerPosiciones(int idEntidad)
        {
            return DB.Posicions.Where(posicion => posicion.idEntidad == idEntidad);
        }

    }
}

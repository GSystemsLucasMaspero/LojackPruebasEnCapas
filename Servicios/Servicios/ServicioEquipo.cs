using Datos.DBContexto;
using Datos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioEquipo
    {
        private RepositorioEquipo repositorio = new RepositorioEquipo();

        public void CrearEquipo(Equipo equipo)
        {
            repositorio.Agregar(equipo);
        }

        public Equipo ObtenerEquipoPorID(int id)
        {
            return repositorio.ObtenerPorID(id);
        }

        public void Modificar(Equipo equipo, int id)
        {
            repositorio.Modificar(equipo,id);
        }

        public IEnumerable<Equipo> ObtenerTodos()
        {
            return repositorio.ObtenerTodos().ToList();
        }

        public void Eliminar(Equipo equipo)
        {
            repositorio.Eliminar(equipo);
        }

        public void EliminarPorID(int id)
        {
            repositorio.Eliminar(ObtenerEquipoPorID(id));
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            return repositorio.ObtenerCuentas();
        }

        public IEnumerable<EquipoTipo> ObtenerTiposDeEquipo()
        {
            return repositorio.ObtenerTiposDeEquipo().ToList();
        }

    }
}

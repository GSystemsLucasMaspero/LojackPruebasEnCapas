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

        public void CrearEquipo(Equipo equipo, int idUsuario)
        {
            repositorio.Agregar(equipo, idUsuario);
        }

        public Equipo ObtenerEquipoPorID(int id)
        {
            return repositorio.ObtenerPorID(id);
        }

        public void Modificar(Equipo equipo, int id, int idUsuario)
        {
            repositorio.Modificar(equipo,id, idUsuario);
        }

        public IQueryable<Equipo> ObtenerTodos()
        {
            return repositorio.ObtenerTodos();
        }

        public void Eliminar(Equipo equipo, int idUsuario)
        {
            repositorio.Eliminar(equipo, idUsuario);
        }

        public void EliminarPorID(int id, int idUsuario)
        {
            repositorio.Eliminar(ObtenerEquipoPorID(id), idUsuario);
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

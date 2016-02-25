using Datos.DBContexto;
using Datos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioEquipoTipo
    {
        private RepositorioEquipoTipo repositorio = new RepositorioEquipoTipo();

        public void CrearEquipoTipo(EquipoTipo equipoTipo)
        {
            repositorio.Agregar(equipoTipo);
        }

        public EquipoTipo ObtenerEquipoTipoPorID(int id)
        {
            return repositorio.ObtenerPorID(id);
        }

        public void Modificar(EquipoTipo equipoTipo, int id)
        {
            repositorio.Modificar(equipoTipo, id);
        }

        public IQueryable<EquipoTipo> ObtenerTodos()
        {
            return repositorio.ObtenerTodos();
        }

        public void Eliminar(EquipoTipo equipoTipo)
        {
            repositorio.Eliminar(equipoTipo);
        }

        public void EliminarPorID(int id)
        {
            repositorio.Eliminar(ObtenerEquipoTipoPorID(id));
        }

    }
}
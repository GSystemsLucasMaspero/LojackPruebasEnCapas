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

        public void CrearEquipoTipo(EquipoTipo equipoTipo, int idUsuario)
        {
            repositorio.Agregar(equipoTipo, idUsuario);
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

        public void Eliminar(EquipoTipo equipoTipo, int idUsuario)
        {
            repositorio.Eliminar(equipoTipo, idUsuario);
        }

        public void EliminarPorID(int id, int idUsuario)
        {
            repositorio.Eliminar(ObtenerEquipoTipoPorID(id),idUsuario);
        }

    }
}
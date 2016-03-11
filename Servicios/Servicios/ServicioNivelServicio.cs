using Datos.DBContexto;
using Datos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioNivelServicio
    {
        private RepositorioNivelServicio repositorio = new RepositorioNivelServicio();

        public void CrearNivelServicio(NivelServicio nivelServicio, int idUsuario)
        {
            repositorio.Agregar(nivelServicio, idUsuario);
        }

        public NivelServicio ObtenerNivelServicioPorID(int id)
        {
            return repositorio.ObtenerPorID(id);
        }

        public void Modificar(NivelServicio nivelServicio, int id)
        {
            repositorio.Modificar(nivelServicio, id);
        }

        public IQueryable<NivelServicio> ObtenerTodos()
        {
            return repositorio.ObtenerTodos();
        }

        public void Eliminar(NivelServicio nivelServicio, int idUsuario)
        {
            repositorio.Eliminar(nivelServicio, idUsuario);
        }

        public void EliminarPorID(int id, int idUsuario)
        {
            repositorio.Eliminar(ObtenerNivelServicioPorID(id), idUsuario);
        }

    }
}

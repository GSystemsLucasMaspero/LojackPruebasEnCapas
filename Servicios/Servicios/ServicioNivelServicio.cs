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

        public void CrearNivelServicio(NivelServicio nivelServicio)
        {
            repositorio.Agregar(nivelServicio);
        }

        public NivelServicio ObtenerNivelServicioPorID(int id)
        {
            return repositorio.ObtenerPorID(id);
        }

        public void Modificar(NivelServicio nivelServicio, int id)
        {
            repositorio.Modificar(nivelServicio, id);
        }

        public IEnumerable<NivelServicio> ObtenerTodos()
        {
            return repositorio.ObtenerTodos().ToList();
        }

        public void Eliminar(NivelServicio nivelServicio)
        {
            repositorio.Eliminar(nivelServicio);
        }

        public void EliminarPorID(int id)
        {
            repositorio.Eliminar(ObtenerNivelServicioPorID(id));
        }

    }
}

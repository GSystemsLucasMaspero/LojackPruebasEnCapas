using Datos.DBContexto;
using Datos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioEntidad
    {
        private RepositorioEntidad repositorio = new RepositorioEntidad();
        private RepositorioPosicion repositorioPos = new RepositorioPosicion();

        public void CrearEntidad(Entidad entidad, int idUsuario)
        {
            repositorio.Agregar(entidad, idUsuario);
        }

        public Entidad ObtenerEntidadPorID(int id)
        {
            return repositorio.ObtenerPorID(id);
        }

        public void Modificar(Entidad entidad, int id, int idUsuario)
        {
            repositorio.Modificar(entidad, id, idUsuario);
        }

        public IQueryable<Entidad> ObtenerTodos()
        {
            return repositorio.ObtenerTodos();
        }

        public IEnumerable<Entidad> ObtenerTodosEnumerable()
        {
            return repositorio.ObtenerTodosEnumerable();
        }

        public void Eliminar(int id, int idUsuario)
        {
            repositorio.Eliminar(this.ObtenerEntidadPorID(id) ,id, idUsuario);
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            return repositorio.ObtenerCuentas();
        }

        public IEnumerable<NivelServicio> ObtenerNivelesDeServicio()
        {
            return repositorio.ObtenerNivelesDeServicio().ToList();
        }

        public bool TienePosicion(int id)
        {
            return repositorioPos.TieneEntidadDeID(id);
        }

        // Devuelve la última (más actual) posición
        public Posicion ObtenerUltimaPosicion(int id)
        {
            return repositorioPos.ObtenerUltimaPosicion(id);
        }

        // Devuelve todas las posiciones
        public IEnumerable<Posicion> ObtenerPosiciones(int id)
        {
            return repositorioPos.ObtenerPosiciones(id);
        }

        public IEnumerable<Entidad> ObtenerEntidadesConPosicion()
        {
            List<Entidad> entidades = new List<Entidad>();
            foreach (int id in repositorioPos.ObtenerIdEntidadConPosicion())
            {
                entidades.Add(ObtenerEntidadPorID(id));
            }
            return entidades;
        }

    }
}

using Datos.DBContexto;
using Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Datos.Repositorios
{
    public class RepositorioNivelServicio : Repositorio<NivelServicio>
    {
        private RepositorioGeneral repositorioGeneral = new RepositorioGeneral();

        public void Agregar(NivelServicio entidad, int idUsuario)
        {
            entidad.fechaAlta = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioAlta = idUsuario;

            base.Agregar(entidad);
        }

        public override void Modificar(NivelServicio entidad, int id)
        {
            NivelServicio nivelServicioToUpdate = this.ObtenerPorID(id);

            nivelServicioToUpdate.descripcion = entidad.descripcion;

            base.Modificar(entidad, id);
        }

        public void Eliminar(NivelServicio entidad, int idUsuario)
        {
            entidad.fechaBaja = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioBaja = idUsuario;

            base.Modificar(entidad, entidad.idNivelServicio);
        }

        public new IQueryable<NivelServicio> ObtenerTodos()
        {
            return base.DBContext.NivelServicios;
        }

    }
}

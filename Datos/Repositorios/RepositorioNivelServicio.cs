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

        public override void Agregar(NivelServicio entidad)
        {
            entidad.fechaAlta = DateTime.Now;
            entidad.usuarioAlta = 20;

            base.Agregar(entidad);
        }

        public override void Modificar(NivelServicio entidad, int id)
        {
            NivelServicio nivelServicioToUpdate = this.ObtenerPorID(id);

            nivelServicioToUpdate.descripcion = entidad.descripcion;

            base.Modificar(entidad, id);
        }

        public void Eliminar(NivelServicio entidad)
        {
            entidad.fechaBaja = DateTime.Now;
            entidad.usuarioBaja = 20;

            base.Modificar(entidad, entidad.idNivelServicio);
        }

        public IEnumerable<NivelServicio> ObtenerTodos()
        {
            return base.DBContext.NivelServicios.ToList();
        }

    }
}

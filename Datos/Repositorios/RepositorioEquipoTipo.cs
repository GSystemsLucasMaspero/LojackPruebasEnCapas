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
    public class RepositorioEquipoTipo : Repositorio<EquipoTipo>
    {

        public override void Agregar(EquipoTipo entidad)
        {
            entidad.fechaAlta = DateTime.Now;
            entidad.usuarioAlta = 20;

            base.Agregar(entidad);
        }

        public override void Modificar(EquipoTipo entidad, int id)
        {
            EquipoTipo equipoTipoToUpdate = this.ObtenerPorID(id);

            equipoTipoToUpdate.descripcion = entidad.descripcion;
            equipoTipoToUpdate.cantSensores = entidad.cantSensores;

            base.Modificar(entidad, id);
        }

        public void Eliminar(EquipoTipo entidad)
        {
            entidad.fechaBaja = DateTime.Now;
            entidad.usuarioBaja = 20;

            base.Modificar(entidad, entidad.idEquipoTipo);
        }

        public IEnumerable<EquipoTipo> ObtenerTodos()
        {
            return base.DBContext.EquipoTipoes.ToList();
        }

    }
}

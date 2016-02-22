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
    public class RepositorioEntidad : Repositorio<Entidad>
    {

        public override void Agregar(Entidad entidad)
        {
            entidad.fechaAlta = DateTime.Now;
            entidad.usuarioAlta = 20;
            entidad.fechaModificacion = DateTime.Now;
            entidad.usuarioModificacion = 20;
            base.Agregar(entidad);
        }

        public override void Modificar(Entidad entidad, int id)
        {
            Entidad entidadToUpdate = this.ObtenerPorID(id);

            entidadToUpdate.nombre = entidad.nombre;
            entidadToUpdate.estado = entidad.estado;
            entidadToUpdate.idNivelServicio = entidad.idNivelServicio;
            entidadToUpdate.plantillaSuceso = entidad.plantillaSuceso;
            entidadToUpdate.cadenciaReporte = entidad.cadenciaReporte;
            entidadToUpdate.comentario = entidad.comentario;
            entidadToUpdate.telefono = entidad.telefono;
            entidadToUpdate.idCuenta = entidad.idCuenta;
            entidadToUpdate.idProcedimiento = entidad.idProcedimiento;

            entidadToUpdate.fechaModificacion = DateTime.Now;
            entidadToUpdate.usuarioModificacion = 20;

            base.Modificar(entidad, id);
        }

        public void Eliminar(Entidad entidad)
        {
            entidad.fechaBaja = DateTime.Now;
            entidad.usuarioBaja = 20;
            entidad.fechaModificacion = DateTime.Now;
            entidad.usuarioModificacion = 20;

            base.Modificar(entidad, entidad.idEntidad);
        }

        public IEnumerable<Entidad> ObtenerTodos()
        {
            return base.DBContext.Entidads.Include(e => e.Cuenta).Include(e => e.NivelServicio).ToList();
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            return base.DBContext.Cuentas.ToList();
        }

        public IEnumerable<NivelServicio> ObtenerNivelesDeServicio()
        {
            return base.DBContext.NivelServicios.ToList();
        }

    }
}

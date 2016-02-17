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
    public class RepositorioEquipo : Repositorio<Equipo>
    {

        public override void Agregar(Equipo entidad)
        {
            entidad.fechaAlta = DateTime.Now;
            entidad.usuarioAlta = 20;
            entidad.fechaModificacion = DateTime.Now;
            entidad.usuarioModificacion = 20;
            base.Modificar(entidad);
        }

        public override void Modificar(Equipo entidad)
        {
            entidad.fechaModificacion = DateTime.Now;
            entidad.usuarioModificacion = 20;
            base.Modificar(entidad);
        }

        public void Eliminar(Equipo entidad)
        {
            entidad.fechaBaja = DateTime.Now;
            entidad.usuarioBaja = 20;
            entidad.fechaModificacion = DateTime.Now;
            entidad.usuarioModificacion = 20;
        }

        public IEnumerable<Equipo> ObtenerTodos()
        {
            return base.DBContext.Equipoes.Include(e => e.Cuenta).Include(e => e.EquipoTipo).ToList();
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            return base.DBContext.Cuentas.ToList();
        }

        public IEnumerable<EquipoTipo> ObtenerTiposDeEquipo()
        {
            return base.DBContext.EquipoTipoes.ToList();
        }

    }
}

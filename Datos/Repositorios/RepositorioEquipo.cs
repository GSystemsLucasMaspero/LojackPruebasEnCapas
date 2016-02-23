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
        private RepositorioGeneral repositorioGeneral = new RepositorioGeneral();

        public override void Agregar(Equipo entidad)
        {
            entidad.fechaAlta = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioAlta = 20;
            entidad.fechaModificacion = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioModificacion = 20;
            base.Agregar(entidad);
        }

        public override void Modificar(Equipo entidad, int id)
        {
            Equipo equipoToUpdate = this.ObtenerPorID(id);

            equipoToUpdate.identificador = entidad.identificador;
            equipoToUpdate.nroSerie = entidad.nroSerie;
            equipoToUpdate.primario = entidad.primario;
            equipoToUpdate.idEquipoTipo = entidad.idEquipoTipo;
            equipoToUpdate.cadencia = entidad.cadencia;
            equipoToUpdate.versionFirmware = entidad.versionFirmware;
            equipoToUpdate.versionProgramacion = entidad.versionProgramacion;
            equipoToUpdate.estadoSd = entidad.estadoSd;
            equipoToUpdate.idCuenta = entidad.idCuenta;
            equipoToUpdate.portable = entidad.portable;
            equipoToUpdate.fechaModificacion = repositorioGeneral.ObtenerDateTimeServer();
            equipoToUpdate.usuarioModificacion = 20;

            base.Modificar(entidad,id);
        }

        public void Eliminar(Equipo entidad)
        {
            entidad.fechaBaja = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioBaja = 20;
            entidad.fechaModificacion = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioModificacion = 20;

            base.Modificar(entidad, entidad.idEquipo);
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

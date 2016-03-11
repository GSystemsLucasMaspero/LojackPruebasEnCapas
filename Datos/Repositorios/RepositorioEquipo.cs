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

        public void Agregar(Equipo entidad, int idUsuario)
        {
            entidad.fechaAlta = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioAlta = idUsuario;
            entidad.fechaModificacion = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioModificacion = idUsuario;
            base.Agregar(entidad);
        }

        public void Modificar(Equipo entidad, int id, int idUsuario)
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
            equipoToUpdate.usuarioModificacion = idUsuario;

            base.Modificar(entidad,id);
        }

        public void Eliminar(Equipo entidad, int idUsuario)
        {
            entidad.fechaBaja = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioBaja = idUsuario;
            entidad.fechaModificacion = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioModificacion = idUsuario;

            base.Modificar(entidad, entidad.idEquipo);
        }

        public new IQueryable<Equipo> ObtenerTodos()
        {
            return base.DBContext.Equipoes.Include(e => e.Cuenta).Include(e => e.EquipoTipo);
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

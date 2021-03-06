﻿using Datos.DBContexto;
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
        private RepositorioGeneral repositorioGeneral = new RepositorioGeneral();

        public void Agregar(Entidad entidad, int idUsuario)
        {
            entidad.fechaAlta = repositorioGeneral.ObtenerDateTimeServer();
            entidad.fechaModificacion = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioAlta = idUsuario;
            entidad.usuarioModificacion = idUsuario;
            base.Agregar(entidad);
        }

        public void Modificar(Entidad entidad, int id, int idUsuario)
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

            entidadToUpdate.fechaModificacion = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioModificacion = idUsuario;

            base.Modificar(entidad, id);
        }

        public void Eliminar(Entidad entidad, int id, int idUsuario)
        {
            entidad.fechaBaja = repositorioGeneral.ObtenerDateTimeServer();
            entidad.fechaModificacion = repositorioGeneral.ObtenerDateTimeServer();
            entidad.usuarioBaja = idUsuario;
            entidad.usuarioModificacion = idUsuario;

            base.Modificar(entidad, id);
        }

        public new Entidad ObtenerPorID(int id)
        {
            return base.DBContext.Set<Entidad>().Include(e => e.Cuenta).Include(e => e.NivelServicio).FirstOrDefault(e => e.idEntidad == id);
        }

        public new IQueryable<Entidad> ObtenerTodos()
        {
            return base.DBContext.Entidads.Include(e => e.Cuenta).Include(e => e.NivelServicio);
        }

        public IEnumerable<Entidad> ObtenerTodosEnumerable()
        {
            return base.DBContext.Entidads.Include(e => e.Cuenta).Include(e => e.NivelServicio);
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            return base.DBContext.Cuentas.ToList();
        }

        public IEnumerable<NivelServicio> ObtenerNivelesDeServicio()
        {
            return base.DBContext.NivelServicios.ToList();
        }

        public int LastID()
        {
            var id = base.DBContext.Database.SqlQuery<Decimal>("SELECT IDENT_CURRENT('Entidad')").AsEnumerable().First();
            return (int)id;
        }

    }
}

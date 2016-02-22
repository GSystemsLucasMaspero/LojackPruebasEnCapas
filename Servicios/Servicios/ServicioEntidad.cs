﻿using Datos.DBContexto;
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

        public void CrearEntidad(Entidad entidad)
        {
            repositorio.Agregar(entidad);
        }

        public Entidad ObtenerEntidadPorID(int id)
        {
            return repositorio.ObtenerPorID(id);
        }

        public void Modificar(Entidad entidad, int id)
        {
            repositorio.Modificar(entidad, id);
        }

        public IEnumerable<Entidad> ObtenerTodos()
        {
            return repositorio.ObtenerTodos().ToList();
        }

        public void Eliminar(Entidad entidad)
        {
            repositorio.Eliminar(entidad);
        }

        public void EliminarPorID(int id)
        {
            repositorio.Eliminar(ObtenerEntidadPorID(id));
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            return repositorio.ObtenerCuentas();
        }

        public IEnumerable<NivelServicio> ObtenerNivelesDeServicio()
        {
            return repositorio.ObtenerNivelesDeServicio().ToList();
        }

    }
}
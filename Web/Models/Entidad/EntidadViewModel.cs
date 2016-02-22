﻿using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Entidad
{
    public class EntidadViewModel
    {
        public int idEntidad { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; }
        public int idNivelServicio { get; set; }
        public string plantillaSuceso { get; set; }
        public int cadenciaReporte { get; set; }
        public System.DateTime fechaAlta { get; set; }
        public int usuarioAlta { get; set; }
        public System.DateTime fechaModificacion { get; set; }
        public int usuarioModificacion { get; set; }
        public Nullable<System.DateTime> fechaBaja { get; set; }
        public Nullable<int> usuarioBaja { get; set; }
        public string comentario { get; set; }
        public string telefono { get; set; }
        public Nullable<int> idProcedimiento { get; set; }
        public Nullable<int> idCuenta { get; set; }

        public virtual Cuenta Cuenta { get; set; }
        public virtual NivelServicio NivelServicio { get; set; }
    }
}
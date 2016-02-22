using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.EquipoTipo
{
    public class EquipoTipoViewModel
    {
        public int idEquipoTipo { get; set; }
        public string descripcion { get; set; }
        public int cantSensores { get; set; }
        public System.DateTime fechaAlta { get; set; }
        public int usuarioAlta { get; set; }
        public Nullable<System.DateTime> fechaBaja { get; set; }
        public Nullable<int> usuarioBaja { get; set; }
    }
}
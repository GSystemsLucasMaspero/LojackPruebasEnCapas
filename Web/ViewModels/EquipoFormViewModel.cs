using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class EquipoFormViewModel
    {
        public string EquipoIdentificador { get; set; }
        public string EquipoNroSerie { get; set; }
        public bool EquipoPrimario { get; set; }
        public EquipoTipo EquipoTipoDeEquipo { get; set; }
        public int EquipoCadencia { get; set; }
        public string EquipoEstadoSD { get; set; }
        public Cuenta EquipoCuenta { get; set; }
        public bool EquipoPortable { get; set; }
    }
}
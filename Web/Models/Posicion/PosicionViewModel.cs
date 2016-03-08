using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Posicion
{
    public class PosicionViewModel
    {
        public int idEntidad { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public double velocidad { get; set; }
    }
}
using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;
using Servicios.Servicios;
using Web.Models;
using AutoMapper;

namespace Web.Controllers
{
    public class EquipoController : Controller
    {
        private ServicioEquipo servicio = new ServicioEquipo();

        public ActionResult Index()
        {
            IEnumerable<EquipoViewModel> viewModelEquipos;
            IEnumerable<Equipo> equipos;

            equipos = servicio.ObtenerTodos();

            viewModelEquipos = Mapper.Map<IEnumerable<Equipo>, IEnumerable<EquipoViewModel>>(equipos);
            return View(viewModelEquipos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                servicio.CrearEquipo(equipo);
                return RedirectToAction("Index");
            }
            return View(equipo);
        }

	}
}
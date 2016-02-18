using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Servicios.Servicios;
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
        public ActionResult Create(EquipoFormViewModel equipoModel)
        {
            Equipo equipo = Mapper.Map<EquipoFormViewModel,Equipo>(equipoModel);
            if (ModelState.IsValid)
            {
                servicio.CrearEquipo(equipo);
                return RedirectToAction("Index");
            }
            return View(equipo);
        }

        public ActionResult Details(int id = 0)
        {
            EquipoViewModel viewModel;
            Equipo equipo;

            equipo = servicio.ObtenerEquipoPorID(id);

            viewModel = Mapper.Map<Equipo,EquipoViewModel>(equipo);
            return View(viewModel);
        }

        public ActionResult Delete(int id = 0)
        {
            EquipoViewModel viewModel;
            Equipo equipo;

            equipo = servicio.ObtenerEquipoPorID(id);

            viewModel = Mapper.Map<Equipo, EquipoViewModel>(equipo);
            return View(viewModel);
        }

        public ActionResult Edit(int id = 0)
        {
            EquipoViewModel viewModel;
            Equipo equipo;

            equipo = servicio.ObtenerEquipoPorID(id);

            viewModel = Mapper.Map<Equipo, EquipoViewModel>(equipo);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                servicio.Modificar(equipo);
                return RedirectToAction("Index");
            }
            return View(equipo);
        }

	}
}
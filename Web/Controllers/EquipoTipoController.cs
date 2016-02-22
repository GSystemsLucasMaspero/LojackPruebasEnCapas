using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Servicios.Servicios;
using AutoMapper;
using PagedList;
using Web.Models.EquipoTipo;

namespace Web.Controllers
{
    public class EquipoTipoController : Controller
    {
        private ServicioEquipoTipo servicio = new ServicioEquipoTipo();

        public ActionResult Index(string search, int page = 1)
        {
            var pageSize = 20;
            IEnumerable<EquipoTipo> equipos = servicio.ObtenerTodos();

            if (search != null)
                page = 1;

            if (!String.IsNullOrEmpty(search))
            {
                equipos = equipos.Where(s => s.descripcion.Contains(search));
            }

            IEnumerable<EquipoTipoViewModel> viewModelEquipoTipos = Mapper.Map<IEnumerable<EquipoTipo>, IEnumerable<EquipoTipoViewModel>>(equipos);
            IPagedList<EquipoTipoViewModel> model = viewModelEquipoTipos.ToPagedList(page, pageSize);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EquipoTipoFormViewModel equipoModel)
        {
            EquipoTipo equipo = Mapper.Map<EquipoTipoFormViewModel, EquipoTipo>(equipoModel);
            if (ModelState.IsValid)
            {
                servicio.CrearEquipoTipo(equipo);
                TempData["AlertMessage"] = "Tipo de Equipo \"" + equipo.descripcion + "\" creado correctamente.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id = 0)
        {
            EquipoTipoViewModel viewModel;
            EquipoTipo equipo;

            equipo = servicio.ObtenerEquipoTipoPorID(id);

            viewModel = Mapper.Map<EquipoTipo, EquipoTipoViewModel>(equipo);
            return View(viewModel);
        }

        public ActionResult Delete(int id = 0)
        {
            EquipoTipoViewModel viewModel;
            EquipoTipo equipo;

            equipo = servicio.ObtenerEquipoTipoPorID(id);

            viewModel = Mapper.Map<EquipoTipo, EquipoTipoViewModel>(equipo);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(EquipoTipoViewModel equipoModel, int id = 0)
        {
            servicio.EliminarPorID(id);
            TempData["AlertMessage"] = "Tipo de Equipo \"" + servicio.ObtenerEquipoTipoPorID(id).descripcion + "\" eliminado correctamente.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            EquipoTipoFormViewModel viewModel;
            EquipoTipo equipo;

            equipo = servicio.ObtenerEquipoTipoPorID(id);

            viewModel = Mapper.Map<EquipoTipo, EquipoTipoFormViewModel>(equipo);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EquipoTipoFormViewModel equipoModel, int id = 0)
        {
            EquipoTipo equipo = Mapper.Map<EquipoTipoFormViewModel, EquipoTipo>(equipoModel);
            if (ModelState.IsValid)
            {
                servicio.Modificar(equipo, id);
                TempData["AlertMessage"] = "Tipo de Equipo \"" + equipo.descripcion + "\" editado correctamente.";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
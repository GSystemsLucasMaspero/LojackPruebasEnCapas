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
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre");
            ViewBag.idEquipoTipo = new SelectList(servicio.ObtenerTiposDeEquipo(), "idEquipoTipo", "descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EquipoFormViewModel equipoModel)
        {
            Equipo equipo = Mapper.Map<EquipoFormViewModel, Equipo>(equipoModel);
            if (ModelState.IsValid)
            {
                servicio.CrearEquipo(equipo);
                TempData["AlertMessage"] = "Equipo \"" + equipo.identificador + "\" creado correctamente.";
                return RedirectToAction("Index");
            }
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre", equipo.idCuenta);
            ViewBag.idEquipoTipo = new SelectList(servicio.ObtenerTiposDeEquipo(), "idEquipoTipo", "descripcion", equipo.idEquipoTipo);
            return View();
        }

        public ActionResult Details(int id = 0)
        {
            EquipoViewModel viewModel;
            Equipo equipo;

            equipo = servicio.ObtenerEquipoPorID(id);

            viewModel = Mapper.Map<Equipo, EquipoViewModel>(equipo);
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

        [HttpPost]
        public ActionResult Delete(EquipoViewModel equipoModel, int id = 0)
        {
            servicio.EliminarPorID(id);
            TempData["AlertMessage"] = "Equipo \"" + servicio.ObtenerEquipoPorID(id).identificador + "\" eliminado correctamente.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            EquipoFormViewModel viewModel;
            Equipo equipo;

            equipo = servicio.ObtenerEquipoPorID(id);

            viewModel = Mapper.Map<Equipo, EquipoFormViewModel>(equipo);

            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre");
            ViewBag.idEquipoTipo = new SelectList(servicio.ObtenerTiposDeEquipo(), "idEquipoTipo", "descripcion");

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EquipoFormViewModel equipoModel, int id = 0)
        {
            Equipo equipo = Mapper.Map<EquipoFormViewModel, Equipo>(equipoModel);
            if (ModelState.IsValid)
            {
                servicio.Modificar(equipo, id);
                TempData["AlertMessage"] = "Equipo \"" + equipo.identificador + "\" editado correctamente.";
                return RedirectToAction("Index");
            }
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre", equipo.idCuenta);
            ViewBag.idEquipoTipo = new SelectList(servicio.ObtenerTiposDeEquipo(), "idEquipoTipo", "descripcion", equipo.idEquipoTipo);
            return View();
        }

    }
}
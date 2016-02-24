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
        private GeneralService servicioGeneral = new GeneralService();

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string search, int page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = "id_asc";
            ViewBag.DescripcionSortParm = String.IsNullOrEmpty(sortOrder) ? "descripcion_asc" : "";
            ViewBag.CantSensoresSortParm = String.IsNullOrEmpty(sortOrder) ? "cantsensores_asc" : "";

            if (search != null)
                page = 1;
            else
                search = currentFilter;

            ViewBag.CurrentFilter = search;

            IEnumerable<EquipoTipo> equipos = servicio.ObtenerTodos();


            if (!String.IsNullOrEmpty(search))
            {
                equipos = equipos.Where(s => s.descripcion.Contains(search));
            }

            switch (sortOrder)
            {
                case "id_asc":
                    equipos = equipos.OrderBy(s => s.idEquipoTipo);
                    break;
                case "descripcion_asc":
                    equipos = equipos.OrderBy(s => s.descripcion);
                    break;
                case "cantsensores_asc":
                    equipos = equipos.OrderBy(s => s.cantSensores);
                    break;
                default:
                    equipos = equipos.OrderBy(s => s.idEquipoTipo);
                    break;
            }

            var pageSize = 20;
            IEnumerable<EquipoTipoViewModel> viewModelEquipoTipos = Mapper.Map<IEnumerable<EquipoTipo>, IEnumerable<EquipoTipoViewModel>>(equipos);
            IPagedList<EquipoTipoViewModel> model = viewModelEquipoTipos.ToPagedList(page, pageSize);

            return View(model); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            EquipoTipoViewModel viewModel;
            EquipoTipo equipo;

            equipo = servicio.ObtenerEquipoTipoPorID(id);
            if (equipo == null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Tipo de Equipo de ID " + id);

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioAlta).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioBaja).FirstOrDefault();

            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<EquipoTipo, EquipoTipoViewModel>(equipo);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            EquipoTipoViewModel viewModel;
            EquipoTipo equipo;

            equipo = servicio.ObtenerEquipoTipoPorID(id);
            if (equipo == null || equipo.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Tipo de Equipo de ID " + id + ". (¿Quizás fue eliminado?)");

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioAlta).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioBaja).FirstOrDefault();

            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<EquipoTipo, EquipoTipoViewModel>(equipo);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EquipoTipoViewModel equipoModel, int id = 0)
        {
            servicio.EliminarPorID(id);
            TempData["AlertMessage"] = "Tipo de Equipo \"" + servicio.ObtenerEquipoTipoPorID(id).descripcion + "\" eliminado correctamente.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            EquipoTipoFormViewModel viewModel;
            EquipoTipo equipo;

            equipo = servicio.ObtenerEquipoTipoPorID(id);
            if (equipo == null || equipo.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Tipo de Equipo de ID " + id + ". (¿Quizás fue eliminado?)");

            viewModel = Mapper.Map<EquipoTipo, EquipoTipoFormViewModel>(equipo);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
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
        #pragma warning disable 612, 618

        private ServicioEquipoTipo servicio = new ServicioEquipoTipo();
        private GeneralService servicioGeneral = new GeneralService();

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string search, int page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = "id_asc";
            ViewBag.DescripcionSortParm = String.IsNullOrEmpty(sortOrder) ? "descripcion_asc" : "";
            ViewBag.CantSensoresSortParm = String.IsNullOrEmpty(sortOrder) ? "cantsensores_asc" : "";

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            if (search != null)
                page = 1;
            else
                search = currentFilter;

            ViewBag.CurrentFilter = search;

            // Setup base query - not evaluated
            IQueryable<EquipoTipo> equipos = servicio.ObtenerTodos();

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

            // Count of all matching records (hits database, but count is relatively quick)
            var equiposCount = equipos.Count();
            // List of current page of 20 records (hits database again, pulls only 20 records, though)
            var equiposList = equipos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Map just the 20 records to view models
            var viewModelEquipoTipos = Mapper.Map<IEnumerable<EquipoTipo>, IEnumerable<EquipoTipoViewModel>>(equiposList);

            // Create StaticPagedList instance to page with
            var model = new StaticPagedList<EquipoTipoViewModel>(viewModelEquipoTipos, page, pageSize, equiposCount);

            return View(model); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");
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

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

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

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

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

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

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
        #pragma warning restore 612, 618
    }
}
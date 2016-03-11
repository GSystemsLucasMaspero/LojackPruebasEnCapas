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

namespace Web.Controllers
{
    public class EquipoController : Controller
    {
        #pragma warning disable 612, 618

        private ServicioEquipo servicio = new ServicioEquipo();
        private GeneralService servicioGeneral = new GeneralService();

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string search, int page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = "id_asc";
            ViewBag.IdentificadorSortParm = String.IsNullOrEmpty(sortOrder) ? "identificador_asc" : "";
            ViewBag.EquipoTipoSortParm = String.IsNullOrEmpty(sortOrder) ? "equipotipo_asc" : "";
            ViewBag.CadenciaSortParm = String.IsNullOrEmpty(sortOrder) ? "cadencia_asc" : "";

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            if (search!= null)
                page = 1;
            else
                search = currentFilter;

            ViewBag.CurrentFilter = search;

            // Setup base query - not evaluated
            IQueryable<Equipo> equipos = servicio.ObtenerTodos();

            if (!String.IsNullOrEmpty(search))
            {
                equipos = equipos.Where(s => s.identificador.Contains(search));
            }

            switch (sortOrder)
            {
                case "id_asc":
                    equipos = equipos.OrderBy(s => s.idEquipo);
                    break;
                case "equipotipo_asc":
                    equipos = equipos.OrderBy(s => s.EquipoTipo.descripcion);
                    break;
                case "cadencia_asc":
                    equipos = equipos.OrderBy(s => s.cadencia);
                    break;
                case "identificador_asc":
                    equipos = equipos.OrderBy(s => s.identificador);
                    break;
                default:
                    equipos = equipos.OrderBy(s => s.idEquipo);
                    break;
            }

            var pageSize = 20;

            // Count of all matching records (hits database, but count is relatively quick)
            var equiposCount = equipos.Count();
            // List of current page of 20 records (hits database again, pulls only 20 records, though)
            var equiposList = equipos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Map just the 20 records to view models
            var viewModelEquipos = Mapper.Map<IEnumerable<Equipo>, IEnumerable<EquipoViewModel>>(equiposList);

            // Create StaticPagedList instance to page with
            var model = new StaticPagedList<EquipoViewModel>(viewModelEquipos, page, pageSize, equiposCount);

            return View(model); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre");
            ViewBag.idEquipoTipo = new SelectList(servicio.ObtenerTiposDeEquipo(), "idEquipoTipo", "descripcion");
            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipoFormViewModel equipoModel)
        {
            Equipo equipo = Mapper.Map<EquipoFormViewModel, Equipo>(equipoModel);
            if (ModelState.IsValid)
            {
                servicio.CrearEquipo(equipo, servicioGeneral.ObtenerUsuarioPorNombre((@Session["user"] as Web.Models.Usuario.UsuarioLogin).UserName).idUsuario);
                TempData["AlertMessage"] = "Equipo \"" + equipo.identificador + "\" creado correctamente.";
                return RedirectToAction("Index");
            }
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre", equipo.idCuenta);
            ViewBag.idEquipoTipo = new SelectList(servicio.ObtenerTiposDeEquipo(), "idEquipoTipo", "descripcion", equipo.idEquipoTipo);
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            EquipoViewModel viewModel;
            Equipo equipo;

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            equipo = servicio.ObtenerEquipoPorID(id);
            if (equipo == null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Equipo de ID " + id);

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioAlta).FirstOrDefault();
            var usuarioModificacionName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioModificacion).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioBaja).FirstOrDefault();
            
            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioModificacion"] = usuarioModificacionName == null ? null : usuarioModificacionName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<Equipo, EquipoViewModel>(equipo);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            EquipoViewModel viewModel;
            Equipo equipo;

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            equipo = servicio.ObtenerEquipoPorID(id);
            if (equipo == null || equipo.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Equipo de ID " + id + ". (¿Quizás fue eliminado?)");

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioAlta).FirstOrDefault();
            var usuarioModificacionName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioModificacion).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioBaja).FirstOrDefault();

            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioModificacion"] = usuarioModificacionName == null ? null : usuarioModificacionName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<Equipo, EquipoViewModel>(equipo);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EquipoViewModel equipoModel, int id = 0)
        {
            servicio.EliminarPorID(id, servicioGeneral.ObtenerUsuarioPorNombre((@Session["user"] as Web.Models.Usuario.UsuarioLogin).UserName).idUsuario);
            TempData["AlertMessage"] = "Equipo \"" + servicio.ObtenerEquipoPorID(id).identificador + "\" eliminado correctamente.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            EquipoFormViewModel viewModel;
            Equipo equipo;

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            equipo = servicio.ObtenerEquipoPorID(id);
            if(equipo == null || equipo.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Equipo de ID " + id + ". (¿Quizás fue eliminado?)");

            viewModel = Mapper.Map<Equipo, EquipoFormViewModel>(equipo);

            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre");
            ViewBag.idEquipoTipo = new SelectList(servicio.ObtenerTiposDeEquipo(), "idEquipoTipo", "descripcion");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EquipoFormViewModel equipoModel, int id = 0)
        {
            Equipo equipo = Mapper.Map<EquipoFormViewModel, Equipo>(equipoModel);
            if (ModelState.IsValid)
            {
                servicio.Modificar(equipo, id, servicioGeneral.ObtenerUsuarioPorNombre((@Session["user"] as Web.Models.Usuario.UsuarioLogin).UserName).idUsuario);
                TempData["AlertMessage"] = "Equipo \"" + equipo.identificador + "\" editado correctamente.";
                return RedirectToAction("Index");
            }
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre", equipo.idCuenta);
            ViewBag.idEquipoTipo = new SelectList(servicio.ObtenerTiposDeEquipo(), "idEquipoTipo", "descripcion", equipo.idEquipoTipo);
            return View(Mapper.Map<Equipo, EquipoFormViewModel>(servicio.ObtenerEquipoPorID(id)));
        }
        #pragma warning restore 612, 618
    }
}
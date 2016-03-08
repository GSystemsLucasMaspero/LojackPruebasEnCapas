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
using Web.Models.Entidad;
using Web.Models.Posicion;

namespace Web.Controllers
{
    public class EntidadController : Controller
    {
#pragma warning disable 612, 618

        private ServicioEntidad servicio = new ServicioEntidad();
        private GeneralService servicioGeneral = new GeneralService();
        private static int lastRouteID = 0;

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string search, int page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = "id_asc";
            ViewBag.NombreSortParm = String.IsNullOrEmpty(sortOrder) ? "nombre_asc" : "";
            ViewBag.EstadoSortParm = String.IsNullOrEmpty(sortOrder) ? "estado_asc" : "";
            ViewBag.NivelServicioSortParm = String.IsNullOrEmpty(sortOrder) ? "nivelservicio_asc" : "";
            ViewBag.CadenciaReporteSortParm = String.IsNullOrEmpty(sortOrder) ? "cadenciareporte_asc" : "";

            if (search != null)
                page = 1;
            else
                search = currentFilter;

            ViewBag.CurrentFilter = search;

            // Setup base query - not evaluated
            IQueryable<Entidad> entidades = servicio.ObtenerTodos();

            if (!String.IsNullOrEmpty(search))
            {
                entidades = entidades.Where(s => s.nombre.Contains(search));
            }

            switch (sortOrder)
            {
                case "id_asc":
                    entidades = entidades.OrderBy(s => s.idEntidad);
                    break;
                case "nombre_asc":
                    entidades = entidades.OrderBy(s => s.nombre);
                    break;
                case "estado_asc":
                    entidades = entidades.OrderBy(s => s.estado);
                    break;
                case "nivelservicio_asc":
                    entidades = entidades.OrderBy(s => s.NivelServicio.descripcion);
                    break;
                case "cadenciareporte_asc":
                    entidades = entidades.OrderBy(s => s.cadenciaReporte);
                    break;
                default:
                    entidades = entidades.OrderBy(s => s.idEntidad);
                    break;
            }

            var pageSize = 20;

            var entidadesCount = entidades.Count();
            var entidadesList = entidades.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var viewModelEntidades = Mapper.Map<IEnumerable<Entidad>, IEnumerable<EntidadViewModel>>(entidadesList);

            foreach (EntidadViewModel entidad in viewModelEntidades)
            {
                if (servicio.TienePosicion(entidad.idEntidad))
                    entidad.TienePosicion = true;
                else
                    entidad.TienePosicion = false;
            }

            var model = new StaticPagedList<EntidadViewModel>(viewModelEntidades, page, pageSize, entidadesCount);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre");
            ViewBag.idNivelServicio = new SelectList(servicio.ObtenerNivelesDeServicio(), "idNivelServicio", "descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntidadFormViewModel entidadModel)
        {
            Entidad entidad = Mapper.Map<EntidadFormViewModel, Entidad>(entidadModel);

            if (ModelState.IsValid)
            {
                servicio.CrearEntidad(entidad);
                TempData["AlertMessage"] = "Entidad \"" + entidad.nombre + "\" creada correctamente.";
                return RedirectToAction("Index");
            }
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre", entidad.idCuenta);
            ViewBag.idNivelServicio = new SelectList(servicio.ObtenerNivelesDeServicio(), "idNivelServicio", "descripcion", entidad.idNivelServicio);
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            EntidadViewModel viewModel;
            Entidad entidad;

            entidad = servicio.ObtenerEntidadPorID(id);
            if (entidad == null)
                return new HttpStatusCodeResult(404, "No se ha encontrado la Entidad de ID " + id);

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioAlta).FirstOrDefault();
            var usuarioModificacionName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioModificacion).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioBaja).FirstOrDefault();

            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioModificacion"] = usuarioModificacionName == null ? null : usuarioModificacionName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<Entidad, EntidadViewModel>(entidad);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            EntidadViewModel viewModel;
            Entidad entidad;

            entidad = servicio.ObtenerEntidadPorID(id);
            if (entidad == null || entidad.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado la Entidad de ID " + id + ". (¿Quizás fue eliminada?)");

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioAlta).FirstOrDefault();
            var usuarioModificacionName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioModificacion).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioBaja).FirstOrDefault();

            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioModificacion"] = usuarioModificacionName == null ? null : usuarioModificacionName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<Entidad, EntidadViewModel>(entidad);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EntidadViewModel entidadModel, int id = 0)
        {
            servicio.EliminarPorID(id);
            TempData["AlertMessage"] = "Entidad \"" + servicio.ObtenerEntidadPorID(id).nombre + "\" eliminada correctamente.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            EntidadFormViewModel viewModel;
            Entidad entidad;

            entidad = servicio.ObtenerEntidadPorID(id);
            if (entidad == null || entidad.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado la Entidad de ID " + id + ". (¿Quizás fue eliminada?)");

            viewModel = Mapper.Map<Entidad, EntidadFormViewModel>(entidad);

            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre");
            ViewBag.idNivelServicio = new SelectList(servicio.ObtenerNivelesDeServicio(), "idNivelServicio", "descripcion");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntidadFormViewModel entidadModel, int id = 0)
        {
            Entidad entidad = Mapper.Map<EntidadFormViewModel, Entidad>(entidadModel);

            if (ModelState.IsValid)
            {
                servicio.Modificar(entidad, id);
                TempData["AlertMessage"] = "Entidad \"" + entidad.nombre + "\" editada correctamente.";
                return RedirectToAction("Index");
            }
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre", entidad.idCuenta);
            ViewBag.idNivelServicio = new SelectList(servicio.ObtenerNivelesDeServicio(), "idNivelServicio", "descripcion", entidad.idNivelServicio);
            return View();
        }

        [HttpGet]
        public ActionResult Locate(int id = 0)
        {
            EntidadViewModel viewModel;
            Entidad entidad;

            entidad = servicio.ObtenerEntidadPorID(id);
            if (entidad == null || !servicio.TienePosicion(entidad.idEntidad))
                return new HttpStatusCodeResult(404, "No se ha encontrado la Entidad de ID " + id);

            viewModel = Mapper.Map<Entidad, EntidadViewModel>(entidad);
            viewModel.TienePosicion = true;
            viewModel.Posicion = servicio.ObtenerUltimaPosicion(entidad.idEntidad);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult LocateAll()
        {
            IEnumerable<Entidad> entidades = servicio.ObtenerEntidadesConPosicion();
            List<EntidadViewModel> viewModelEntidades = new List<EntidadViewModel>();

            foreach (Entidad e in entidades)
            {
                EntidadViewModel model = Mapper.Map<Entidad, EntidadViewModel>(e);
                model.Posicion = servicio.ObtenerUltimaPosicion(e.idEntidad);
                viewModelEntidades.Add(model);
            }

            return View(viewModelEntidades);
        }

        [HttpGet]
        public ActionResult Route(int id = 0)
        {
            lastRouteID = id;
            IEnumerable<Posicion> results = servicio.ObtenerPosiciones(lastRouteID);
            if(id <= 0 || results == null)
                return new HttpStatusCodeResult(404, "No se ha encontrado la Entidad de ID " + id + ". (¿Quizás fue eliminada?)");
            return View(Mapper.Map<IEnumerable<Posicion>, IEnumerable<PosicionViewModel>>(results));
        }

#pragma warning restore 612, 618

        [HttpGet]
        public ActionResult Speed(int id = 0)
        {

            return View();
        }

        public JsonResult GetPositions(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = servicio.ObtenerPosiciones(lastRouteID).Select(
                    a => new
                    {
                        a.idPosicion,
                        a.fechaPosicion,
                        a.latitud,
                        a.longitud,
                        a.velocidad,
                    });
            int totalRecords = results.Count();

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                results = results.OrderByDescending(s => s.fechaPosicion);
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                results = results.OrderBy(s => s.fechaPosicion);
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

    }
}
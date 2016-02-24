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
using Web.Models.NivelServicio;

namespace Web.Controllers
{
    public class NivelServicioController : Controller
    {
        private ServicioNivelServicio servicio = new ServicioNivelServicio();
        private GeneralService servicioGeneral = new GeneralService();

        public ActionResult Index(string sortOrder, string currentFilter, string search, int page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = "id_asc";
            ViewBag.DescripcionSortParm = String.IsNullOrEmpty(sortOrder) ? "descripcion_asc" : "";

            if (search != null)
                page = 1;
            else
                search = currentFilter;

            ViewBag.CurrentFilter = search;

            IEnumerable<NivelServicio> servicios = servicio.ObtenerTodos();


            if (!String.IsNullOrEmpty(search))
            {
                servicios = servicios.Where(s => s.descripcion.Contains(search));
            }

            switch (sortOrder)
            {
                case "id_asc":
                    servicios = servicios.OrderBy(s => s.idNivelServicio);
                    break;
                case "descripcion_asc":
                    servicios = servicios.OrderBy(s => s.descripcion);
                    break;
                default:
                    servicios = servicios.OrderBy(s => s.idNivelServicio);
                    break;
            }

            var pageSize = 20;
            IEnumerable<NivelServicioViewModel> viewModelNivelServicio = Mapper.Map<IEnumerable<NivelServicio>, IEnumerable<NivelServicioViewModel>>(servicios);
            IPagedList<NivelServicioViewModel> model = viewModelNivelServicio.ToPagedList(page, pageSize);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NivelServicioFormViewModel servicioModel)
        {
            NivelServicio service = Mapper.Map<NivelServicioFormViewModel, NivelServicio>(servicioModel);
            if (ModelState.IsValid)
            {
                servicio.CrearNivelServicio(service);
                TempData["AlertMessage"] = "Nivel de Servicio \"" + service.descripcion + "\" creado correctamente.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id = 0)
        {
            NivelServicioViewModel viewModel;
            NivelServicio service;

            service = servicio.ObtenerNivelServicioPorID(id);
            if (service == null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Nivel de Servicio de ID " + id);

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == service.usuarioAlta).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == service.usuarioBaja).FirstOrDefault();

            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<NivelServicio, NivelServicioViewModel>(service);
            return View(viewModel);
        }

        public ActionResult Delete(int id = 0)
        {
            NivelServicioViewModel viewModel;
            NivelServicio service;

            service = servicio.ObtenerNivelServicioPorID(id);
            if (service == null || service.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Nivel de Servicio de ID " + id + ". (¿Quizás fue eliminado?)");

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == service.usuarioAlta).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == service.usuarioBaja).FirstOrDefault();

            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<NivelServicio, NivelServicioViewModel>(service);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(NivelServicioViewModel serviceModel, int id = 0)
        {
            servicio.EliminarPorID(id);
            TempData["AlertMessage"] = "Nivel de Servicio \"" + servicio.ObtenerNivelServicioPorID(id).descripcion + "\" eliminado correctamente.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            NivelServicioFormViewModel viewModel;
            NivelServicio service;

            service = servicio.ObtenerNivelServicioPorID(id);
            if (service == null || service.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Nivel de Servicio de ID " + id + ". (¿Quizás fue eliminado?)");

            viewModel = Mapper.Map<NivelServicio, NivelServicioFormViewModel>(service);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(NivelServicioFormViewModel serviceModel, int id = 0)
        {
            NivelServicio service = Mapper.Map<NivelServicioFormViewModel, NivelServicio>(serviceModel);
            if (ModelState.IsValid)
            {
                servicio.Modificar(service, id);
                TempData["AlertMessage"] = "Nivel de Servicio \"" + service.descripcion + "\" editado correctamente.";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
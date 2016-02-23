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

namespace Web.Controllers
{
    public class EntidadController : Controller
    {
        private ServicioEntidad servicio = new ServicioEntidad();
        private GeneralService servicioGeneral = new GeneralService();

        public ActionResult Index(string search, int page = 1)
        {
            var pageSize = 20;
            IEnumerable<Entidad> entidades = servicio.ObtenerTodos();

            if (search != null)
                page = 1;

            if (!String.IsNullOrEmpty(search))
            {
                entidades = entidades.Where(s => s.nombre.Contains(search));
            }

            IEnumerable<EntidadViewModel> viewModelEntidades = Mapper.Map<IEnumerable<Entidad>, IEnumerable<EntidadViewModel>>(entidades);
            IPagedList<EntidadViewModel> model = viewModelEntidades.ToPagedList(page, pageSize);

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre");
            ViewBag.idNivelServicio = new SelectList(servicio.ObtenerNivelesDeServicio(), "idNivelServicio", "descripcion");
            return View();
        }

        [HttpPost]
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

        public ActionResult Details(int id = 0)
        {
            EntidadViewModel viewModel;
            Entidad entidad;

            entidad = servicio.ObtenerEntidadPorID(id);

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioAlta).FirstOrDefault();
            var usuarioModificacionName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioModificacion).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == entidad.usuarioBaja).FirstOrDefault();

            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioModificacion"] = usuarioModificacionName == null ? null : usuarioModificacionName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<Entidad, EntidadViewModel>(entidad);
            return View(viewModel);
        }

        public ActionResult Delete(int id = 0)
        {
            EntidadViewModel viewModel;
            Entidad entidad;

            entidad = servicio.ObtenerEntidadPorID(id);

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
        public ActionResult Delete(EntidadViewModel entidadModel, int id = 0)
        {
            servicio.EliminarPorID(id);
            TempData["AlertMessage"] = "Entidad \"" + servicio.ObtenerEntidadPorID(id).nombre + "\" eliminada correctamente.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            EntidadFormViewModel viewModel;
            Entidad entidad;

            entidad = servicio.ObtenerEntidadPorID(id);

            viewModel = Mapper.Map<Entidad, EntidadFormViewModel>(entidad);

            ViewBag.idCuenta = new SelectList(servicio.ObtenerCuentas(), "idCuenta", "nombre");
            ViewBag.idNivelServicio = new SelectList(servicio.ObtenerNivelesDeServicio(), "idNivelServicio", "descripcion");

            return View(viewModel);
        }

        [HttpPost]
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

    }
}
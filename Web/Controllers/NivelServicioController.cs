﻿using Datos.DBContexto;
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
        #pragma warning disable 612, 618

        private ServicioNivelServicio servicio = new ServicioNivelServicio();
        private GeneralService servicioGeneral = new GeneralService();

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string search, int page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = "id_asc";
            ViewBag.DescripcionSortParm = String.IsNullOrEmpty(sortOrder) ? "descripcion_asc" : "";

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            if (search != null)
                page = 1;
            else
                search = currentFilter;

            ViewBag.CurrentFilter = search;
            
            // Setup base query - not evaluated
            IQueryable<NivelServicio> servicios = servicio.ObtenerTodos();

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

            // Count of all matching records (hits database, but count is relatively quick)
            var serviciosCount = servicios.Count();
            // List of current page of 20 records (hits database again, pulls only 20 records, though)
            var serviciosList = servicios.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Map just the 20 records to view models
            var viewModelServicios = Mapper.Map<IEnumerable<NivelServicio>, IEnumerable<NivelServicioViewModel>>(serviciosList);

            // Create StaticPagedList instance to page with
            var model = new StaticPagedList<NivelServicioViewModel>(viewModelServicios, page, pageSize, serviciosCount);

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
        public ActionResult Create(NivelServicioFormViewModel servicioModel)
        {
            NivelServicio service = Mapper.Map<NivelServicioFormViewModel, NivelServicio>(servicioModel);
            if (ModelState.IsValid)
            {
                servicio.CrearNivelServicio(service, servicioGeneral.ObtenerUsuarioPorNombre((@Session["user"] as Web.Models.Usuario.UsuarioLogin).UserName).idUsuario);
                TempData["AlertMessage"] = "Nivel de Servicio \"" + service.descripcion + "\" creado correctamente.";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            NivelServicioViewModel viewModel;
            NivelServicio service;

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

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

        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            NivelServicioViewModel viewModel;
            NivelServicio service;

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(NivelServicioViewModel serviceModel, int id = 0)
        {
            servicio.EliminarPorID(id, servicioGeneral.ObtenerUsuarioPorNombre((@Session["user"] as Web.Models.Usuario.UsuarioLogin).UserName).idUsuario);
            TempData["AlertMessage"] = "Nivel de Servicio \"" + servicio.ObtenerNivelServicioPorID(id).descripcion + "\" eliminado correctamente.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            NivelServicioFormViewModel viewModel;
            NivelServicio service;

            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");

            service = servicio.ObtenerNivelServicioPorID(id);
            if (service == null || service.usuarioBaja != null)
                return new HttpStatusCodeResult(404, "No se ha encontrado el Nivel de Servicio de ID " + id + ". (¿Quizás fue eliminado?)");

            viewModel = Mapper.Map<NivelServicio, NivelServicioFormViewModel>(service);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        #pragma warning restore 612, 618
    }
}
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

namespace Web.Controllers
{
    public class EquipoController : Controller
    {
        private ServicioEquipo servicio = new ServicioEquipo();
        private GeneralService servicioGeneral = new GeneralService();

        public ActionResult Index(string search, int page = 1)
        {
            var pageSize = 20;
            IEnumerable<Equipo> equipos = servicio.ObtenerTodos();

            if (search != null)
                page = 1;

            if (!String.IsNullOrEmpty(search))
            {
                equipos = equipos.Where(s => s.identificador.Contains(search));
            }

            IEnumerable<EquipoViewModel> viewModelEquipos = Mapper.Map<IEnumerable<Equipo>, IEnumerable<EquipoViewModel>>(equipos);
            IPagedList<EquipoViewModel> model = viewModelEquipos.ToPagedList(page,pageSize);

            return View(model); 
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

            var usuarioAltaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioAlta).FirstOrDefault();
            var usuarioModificacionName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioModificacion).FirstOrDefault();
            var usuarioBajaName = servicioGeneral.ObtenerUsuarios().Where(a => a.idUsuario == equipo.usuarioBaja).FirstOrDefault();
            
            TempData["UsuarioAlta"] = usuarioAltaName == null ? null : usuarioAltaName.userLogin;
            TempData["UsuarioModificacion"] = usuarioModificacionName == null ? null : usuarioModificacionName.userLogin;
            TempData["UsuarioBaja"] = usuarioBajaName == null ? null : usuarioBajaName.userLogin;

            viewModel = Mapper.Map<Equipo, EquipoViewModel>(equipo);
            return View(viewModel);
        }

        public ActionResult Delete(int id = 0)
        {
            EquipoViewModel viewModel;
            Equipo equipo;

            equipo = servicio.ObtenerEquipoPorID(id);

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
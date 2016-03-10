using Datos.DBContexto;
using Servicios.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Usuario;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private ServicioPassword servicio = new ServicioPassword();
        private GeneralService servicioGeneral = new GeneralService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioLogin model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = servicioGeneral.ObtenerUsuarioPorNombre(model.UserName);
                if (usuario != null && model.UserName.Equals(usuario.userLogin) && usuario.password.Equals(servicio.Encrypt(model.Password)))
                {
                    Session["user"] = new UsuarioLogin() { UserName = model.UserName , nombre = usuario.nombre , apellido = usuario.apellido };
                    return RedirectToAction("UserPanel","Account");
                }
            } 
            
                
            return View();
        }

        public ActionResult UserPanel()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

	}
}
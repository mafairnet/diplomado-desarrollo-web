using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["contenido"] = "Este es el contenido de mi pagina web...";
            ViewBag.Title = "Pagina Principal";
            ViewBag.Message = "Aqui se listan los usuarios dados de alta en la aplicacion.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "Acerca de..";
            ViewBag.Message = "Esta aplicacion es una aplicacionq ue nos va a permitir dar de alta, modificar y dar de baja usuarios.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contacto";
            ViewBag.Message = "Nos puedes contactar al correo mtorres@universidadaztlan.com";

            return View();
        }
    }
}
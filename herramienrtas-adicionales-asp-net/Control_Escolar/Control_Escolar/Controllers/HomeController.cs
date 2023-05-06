using Control_Escolar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Control_Escolar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = "https://localhost:7227/api/";
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Usuario");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    var jsonSettings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    catalogoUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(content.Result, jsonSettings);
                }
            }

            var isValidUser = false;

            foreach (Usuario usuarioRegistrado in catalogoUsuarios) {

                if (usuarioRegistrado.Correo == usuario.Correo && usuarioRegistrado.Contrasena == usuario.Contrasena) {
                    isValidUser = true;
                }
            }

            if (isValidUser) {
                FormsAuthentication.SetAuthCookie(usuario.Correo, false);
                return RedirectToAction("Index", "Usuario");
            }

            ModelState.AddModelError("", "Usuario o Contraseña Invalidos");

            return View();
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
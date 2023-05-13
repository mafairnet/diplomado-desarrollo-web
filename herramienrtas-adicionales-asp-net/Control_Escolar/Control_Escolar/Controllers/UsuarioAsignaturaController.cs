using Control_Escolar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Control_Escolar.Controllers
{
    [Authorize]
    public class UsuarioAsignaturaController : Controller
    {
        public ActionResult Index(int ID) {

            var catalogoUsuarios = new List<Usuario>();
            var catalogoAsignaturas = new List<Asignatura>();
            var catalogoStatus = new List<Status>();
            var catalogoUsuarioAsignaturas = new List<UsuarioAsignatura>();
            

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Asignatura");
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

                    catalogoAsignaturas = JsonConvert.DeserializeObject<List<Asignatura>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Status");
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

                    catalogoStatus = JsonConvert.DeserializeObject<List<Status>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("UsuarioAsignatura");
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

                    catalogoUsuarioAsignaturas = JsonConvert.DeserializeObject<List<UsuarioAsignatura>>(content.Result, jsonSettings);
                }
            }

            var usuario = catalogoUsuarios.Where(x => x.ID == ID).First();

            var asignaturasUsuario = new List<Asignatura>();

            foreach (UsuarioAsignatura usuarioAsignatura in catalogoUsuarioAsignaturas)
            {
                foreach (Asignatura asignatura in catalogoAsignaturas)
                {
                    if (usuarioAsignatura.IdUsuario == usuario.ID && usuarioAsignatura.IdAsignatura == asignatura.ID)
                    {
                        foreach (Status status in catalogoStatus) {
                            if (asignatura.IdStatus == status.ID) {
                                asignatura.Status = status;
                            }
                        }
                        asignaturasUsuario.Add(asignatura);
                    }
                }
            }
            usuario.Asignaturas = asignaturasUsuario;

            ViewData["usuario"] = usuario;
            return View();
        }

        public ActionResult Add(int ID) {
            var usuarioAsignatura = new UsuarioAsignatura();
            var catalogoAsignaturas = new List<Asignatura>();
            var catalogoUsuarios = new List<Usuario>();
            var catalogoUsuarioAsignaturas = new List<UsuarioAsignatura>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Asignatura");
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

                    catalogoAsignaturas = JsonConvert.DeserializeObject<List<Asignatura>>(content.Result, jsonSettings);
                }
            }

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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("UsuarioAsignatura");
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

                    catalogoUsuarioAsignaturas = JsonConvert.DeserializeObject<List<UsuarioAsignatura>>(content.Result, jsonSettings);
                }
            }

            var catalogoAsignaturasNoAsignadas = new List<Asignatura>();

            foreach (Asignatura asignatura in catalogoAsignaturas)
            {
                var asignada = false;
                foreach (UsuarioAsignatura usuarioAsignaturaItem in catalogoUsuarioAsignaturas)
                {
                    if (asignatura.ID == usuarioAsignaturaItem.IdAsignatura && usuarioAsignaturaItem.IdUsuario == ID)
                    {
                        asignada = true;
                    }
                }
                if (!asignada) {
                    catalogoAsignaturasNoAsignadas.Add(asignatura);
                }
            }


            var usuario = catalogoUsuarios.Where(x => x.ID == ID).First();

            ViewData["catalogoAsignaturas"] = catalogoAsignaturasNoAsignadas;
            ViewBag.asignaturas = new SelectList(catalogoAsignaturasNoAsignadas, "id", "nombre");

            usuarioAsignatura.IdUsuario = ID;

            ViewData["usuarioAsignatura"] = usuarioAsignatura;
            ViewData["usuario"] = usuario;
            return View();
        }

        [HttpPost]
        public ActionResult Add(UsuarioAsignatura usuarioAsignatura) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(usuarioAsignatura, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("usuarioAsignatura/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    usuarioAsignatura = JsonConvert.DeserializeObject<UsuarioAsignatura>(content.Result, jsonSettings);
                }
            }
            
            return RedirectToAction("Index/" + usuarioAsignatura.IdUsuario);
        }

        
        public ActionResult Delete(String ID)
        {
            String[] idArray = ID.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int idAsignatura = int.Parse(idArray[1]);

            var usuarioAsignatura = new UsuarioAsignatura();
            var catalogoAsignaturas = new List<Asignatura>();
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Asignatura");
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

                    catalogoAsignaturas = JsonConvert.DeserializeObject<List<Asignatura>>(content.Result, jsonSettings);
                }
            }

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

            var usuario = catalogoUsuarios.Where(x => x.ID == idUsuario).First();
            var asignatura = catalogoAsignaturas.Where(x => x.ID == idAsignatura).First();

            ViewData["catalogoAsignaturas"] = catalogoAsignaturas;
            ViewBag.asignaturas = new SelectList(catalogoAsignaturas, "id", "nombre");

            usuarioAsignatura.IdUsuario = idUsuario;
            usuarioAsignatura.IdAsignatura = idAsignatura;


            ViewData["usuarioAsignatura"] = usuarioAsignatura;
            ViewData["usuario"] = usuario;

            return View();
        }

        [HttpPost]
        public ActionResult Delete(UsuarioAsignatura usuarioAsignatura)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("usuarioAsignatura/" + usuarioAsignatura.IdUsuario + "_" + usuarioAsignatura.IdAsignatura);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                }
            }
            return RedirectToAction("Index/" + usuarioAsignatura.IdUsuario);
        }

    }
}
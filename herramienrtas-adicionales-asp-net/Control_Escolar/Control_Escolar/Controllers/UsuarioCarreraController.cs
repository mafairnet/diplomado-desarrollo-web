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
    public class UsuarioCarreraController : Controller
    {
        public ActionResult Index(int ID) {

            var catalogoUsuarios = new List<Usuario>();
            var catalogoCarreras = new List<Carrera>();
            var catalogoStatus = new List<Status>();
            var catalogoUsuarioCarreras = new List<UsuarioCarrera>();
            

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
                var responseTask = client.GetAsync("Carrera");
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

                    catalogoCarreras = JsonConvert.DeserializeObject<List<Carrera>>(content.Result, jsonSettings);
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
                var responseTask = client.GetAsync("UsuarioCarrera");
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

                    catalogoUsuarioCarreras = JsonConvert.DeserializeObject<List<UsuarioCarrera>>(content.Result, jsonSettings);
                }
            }

            var usuario = catalogoUsuarios.Where(x => x.ID == ID).First();

            var carrerasUsuario = new List<Carrera>();

            foreach (UsuarioCarrera usuarioCarrera in catalogoUsuarioCarreras)
            {
                foreach (Carrera carrera in catalogoCarreras)
                {
                    if (usuarioCarrera.IdUsuario == usuario.ID && usuarioCarrera.IdCarrera == carrera.ID)
                    {
                        foreach (Status status in catalogoStatus) {
                            if (carrera.IdStatus == status.ID) {
                                carrera.Status = status;
                            }
                        }
                        carrerasUsuario.Add(carrera);
                    }
                }
            }
            usuario.Carreras = carrerasUsuario;

            ViewData["usuario"] = usuario;
            return View();
        }

        public ActionResult Add(int ID) {
            var usuarioCarrera = new UsuarioCarrera();
            var catalogoCarreras = new List<Carrera>();
            var catalogoUsuarios = new List<Usuario>();
            var catalogoUsuarioCarreras = new List<UsuarioCarrera>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Carrera");
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

                    catalogoCarreras = JsonConvert.DeserializeObject<List<Carrera>>(content.Result, jsonSettings);
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
                var responseTask = client.GetAsync("UsuarioCarrera");
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

                    catalogoUsuarioCarreras = JsonConvert.DeserializeObject<List<UsuarioCarrera>>(content.Result, jsonSettings);
                }
            }

            var catalogoCarrerasNoAsignadas = new List<Carrera>();

            foreach (Carrera carrera in catalogoCarreras)
            {
                var asignada = false;
                foreach (UsuarioCarrera usuarioCarreraItem in catalogoUsuarioCarreras)
                {
                    if (carrera.ID == usuarioCarreraItem.IdCarrera && usuarioCarreraItem.IdUsuario == ID)
                    {
                        asignada = true;
                    }
                }
                if (!asignada) {
                    catalogoCarrerasNoAsignadas.Add(carrera);
                }
            }


            var usuario = catalogoUsuarios.Where(x => x.ID == ID).First();

            ViewData["catalogoCarreras"] = catalogoCarrerasNoAsignadas;
            ViewBag.carreras = new SelectList(catalogoCarrerasNoAsignadas, "id", "nombre");

            usuarioCarrera.IdUsuario = ID;

            ViewData["usuarioCarrera"] = usuarioCarrera;
            ViewData["usuario"] = usuario;
            return View();
        }

        [HttpPost]
        public ActionResult Add(UsuarioCarrera usuarioCarrera) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(usuarioCarrera, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("usuarioCarrera/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    usuarioCarrera = JsonConvert.DeserializeObject<UsuarioCarrera>(content.Result, jsonSettings);
                }
            }
            
            return RedirectToAction("Index/" + usuarioCarrera.IdUsuario);
        }

        
        public ActionResult Delete(String ID)
        {
            String[] idArray = ID.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int idCarrera = int.Parse(idArray[1]);

            var usuarioCarrera = new UsuarioCarrera();
            var catalogoCarreras = new List<Carrera>();
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Carrera");
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

                    catalogoCarreras = JsonConvert.DeserializeObject<List<Carrera>>(content.Result, jsonSettings);
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
            var carrera = catalogoCarreras.Where(x => x.ID == idCarrera).First();

            ViewData["catalogoCarreras"] = catalogoCarreras;
            ViewBag.carreras = new SelectList(catalogoCarreras, "id", "nombre");

            usuarioCarrera.IdUsuario = idUsuario;
            usuarioCarrera.IdCarrera = idCarrera;


            ViewData["usuarioCarrera"] = usuarioCarrera;
            ViewData["usuario"] = usuario;

            return View();
        }

        [HttpPost]
        public ActionResult Delete(UsuarioCarrera usuarioCarrera)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("usuarioCarrera/" + usuarioCarrera.IdUsuario + "_" + usuarioCarrera.IdCarrera);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                }
            }
            return RedirectToAction("Index/" + usuarioCarrera.IdUsuario);
        }

    }
}
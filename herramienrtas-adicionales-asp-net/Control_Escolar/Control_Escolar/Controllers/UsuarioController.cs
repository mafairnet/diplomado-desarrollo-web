using Control_Escolar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Control_Escolar.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        
        public ActionResult Index() {

            var catalogoStatuses = new List<Status>();
            var catalogoUbicaciones = new List<Ubicacion>();
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];



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

                    catalogoStatuses = JsonConvert.DeserializeObject<List<Status>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Ubicacion");
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

                    catalogoUbicaciones = JsonConvert.DeserializeObject<List<Ubicacion>>(content.Result, jsonSettings);
                }
            }


            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Usuario");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    var jsonSettings = new JsonSerializerSettings {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    catalogoUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(content.Result, jsonSettings);
                }
            }

            //var tempCatalogoUsuarios = new List<Usuario>();

            foreach (Usuario usuario in catalogoUsuarios) {

                var newStatus = new Status();
                newStatus.Nombre = "No definido";
                usuario.Status = newStatus;

                foreach (Status status in catalogoStatuses) {
                    if (usuario.IdStatus == status.ID)
                    {
                        usuario.Status = status;
                    }
                }

                var newUbicacion = new Ubicacion();
                newUbicacion.Nombre = "No definido";
                usuario.Ubicacion = newUbicacion;

                foreach (Ubicacion ubicacion in catalogoUbicaciones)
                {
                    if (usuario.IdUbicacion == ubicacion.ID)
                    {
                        usuario.Ubicacion = ubicacion;
                    }
                }
            }



            ViewData["catalogoStatuses"] = catalogoStatuses;
            ViewBag.statuses = new SelectList(catalogoStatuses, "id", "nombre");

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            ViewBag.ubicaciones = new SelectList(catalogoUbicaciones, "id", "nombre");

            ViewData["catalogoUsuarios"] = catalogoUsuarios;
            return View();
        }

 
        public ActionResult Add() {
            var usuario = new Usuario();
            var catalogoStatuses = new List<Status>();
            var catalogoUbicaciones = new List<Ubicacion>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

                    catalogoStatuses = JsonConvert.DeserializeObject<List<Status>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Ubicacion");
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

                    catalogoUbicaciones = JsonConvert.DeserializeObject<List<Ubicacion>>(content.Result, jsonSettings);
                }
            }

            ViewData["usuario"] = usuario;
            ViewData["catalogoStatuses"] = catalogoStatuses;

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            ViewBag.ubicaciones = new SelectList(catalogoUbicaciones, "id", "nombre");

            ViewBag.statuses = new SelectList(catalogoStatuses, "id", "nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Add(Usuario usuario) {

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(usuario, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("usuario/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(content.Result, jsonSettings);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var usuario = new Usuario();

            var catalogoStatuses = new List<Status>();
            var catalogoUbicaciones = new List<Ubicacion>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

                    catalogoStatuses = JsonConvert.DeserializeObject<List<Status>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Ubicacion");
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

                    catalogoUbicaciones = JsonConvert.DeserializeObject<List<Ubicacion>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Usuario/"+ID);
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

                    usuario = JsonConvert.DeserializeObject<Usuario>(content.Result, jsonSettings);
                }
            }

            foreach (Status status in catalogoStatuses)
            {
                if (usuario.IdStatus == status.ID)
                {
                    usuario.Status = status;
                }
            }

            foreach (Ubicacion ubicacion in catalogoUbicaciones)
            {
                if (usuario.IdUbicacion == ubicacion.ID)
                {
                    usuario.Ubicacion = ubicacion;
                }
            }

            ViewData["catalogoStatuses"] = catalogoStatuses;
            ViewBag.statuses = new SelectList(catalogoStatuses, "id", "nombre");

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            ViewBag.ubicaciones = new SelectList(catalogoUbicaciones, "id", "nombre");

            ViewData["usuario"] = usuario;
            return View();
        }


        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(usuario,jsonSettings);
            
            
            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var putTask = client.PutAsync("usuario/" + usuario.ID, payloadContent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(content.Result, jsonSettings);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var usuario = new Usuario();

            var catalogoStatuses = new List<Status>();
            var catalogoUbicaciones = new List<Ubicacion>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

                    catalogoStatuses = JsonConvert.DeserializeObject<List<Status>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Ubicacion");
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

                    catalogoUbicaciones = JsonConvert.DeserializeObject<List<Ubicacion>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Usuario/" + ID);
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

                    usuario = JsonConvert.DeserializeObject<Usuario>(content.Result, jsonSettings);
                }
            }

            foreach (Status status in catalogoStatuses)
            {
                if (usuario.IdStatus == status.ID)
                {
                    usuario.Status = status;
                }
            }

            foreach (Ubicacion ubicacion in catalogoUbicaciones)
            {
                if (usuario.IdUbicacion == ubicacion.ID)
                {
                    usuario.Ubicacion = ubicacion;
                }
            }

            ViewData["catalogoStatuses"] = catalogoStatuses;
            ViewBag.statuses = new SelectList(catalogoStatuses, "id", "nombre");

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            ViewBag.ubicaciones = new SelectList(catalogoUbicaciones, "id", "nombre");

            ViewData["usuario"] = usuario;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Usuario usuario)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("usuario/" + usuario.ID);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    
                }
            }
            return RedirectToAction("Index");
        }

    }
}
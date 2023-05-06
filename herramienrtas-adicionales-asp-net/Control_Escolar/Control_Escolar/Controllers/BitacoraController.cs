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
    public class BitacoraController : Controller
    {
        public ActionResult Index() {
            var catalogoBitacoras = new List<Bitacora>();
            var catalogoAcciones = new List<Accion>();
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Accion");
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

                    catalogoAcciones = JsonConvert.DeserializeObject<List<Accion>>(content.Result, jsonSettings);
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
                var responseTask = client.GetAsync("Bitacora");
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

                    catalogoBitacoras = JsonConvert.DeserializeObject<List<Bitacora>>(content.Result, jsonSettings);
                }
            }

            foreach (Bitacora bitacora in catalogoBitacoras)
            {

                var newAccion = new Accion();
                newAccion.Nombre = "No definido";
                bitacora.Accion = newAccion;

                foreach (Accion accion in catalogoAcciones)
                {
                    if (bitacora.IdAccion == accion.ID)
                    {
                        bitacora.Accion = accion;
                    }
                }

                foreach (Usuario usuario in catalogoUsuarios)
                {
                    if (bitacora.IdUsuario == usuario.ID)
                    {
                        bitacora.Usuario = usuario;
                    }
                }
            }

            ViewData["catalogoAcciones"] = catalogoAcciones;
            ViewBag.acciones = new SelectList(catalogoAcciones, "id", "nombre");

            ViewData["catalogoUsuarios"] = catalogoUsuarios;
            ViewBag.usuarios = new SelectList(catalogoUsuarios, "id", "nombre");

            ViewData["catalogoBitacoras"] = catalogoBitacoras;
            return View();
        }

        public ActionResult Add() {
            var bitacora = new Bitacora();
            var catalogoAcciones = new List<Accion>();
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Accion");
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

                    catalogoAcciones = JsonConvert.DeserializeObject<List<Accion>>(content.Result, jsonSettings);
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

            ViewData["catalogoAcciones"] = catalogoAcciones;
            ViewBag.acciones = new SelectList(catalogoAcciones, "id", "nombre");

            ViewData["catalogoUsuarios"] = catalogoUsuarios;
            ViewBag.usuarios = new SelectList(catalogoUsuarios, "id", "nombre");

            ViewData["bitacora"] = bitacora;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Bitacora bitacora) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(bitacora, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("bitacora/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    bitacora = JsonConvert.DeserializeObject<Bitacora>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var bitacora = new Bitacora();
            var catalogoAcciones = new List<Accion>();
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Accion");
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

                    catalogoAcciones = JsonConvert.DeserializeObject<List<Accion>>(content.Result, jsonSettings);
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
                var responseTask = client.GetAsync("bitacora/" + ID);
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

                    bitacora = JsonConvert.DeserializeObject<Bitacora>(content.Result, jsonSettings);
                }
            }

            foreach (Accion accion in catalogoAcciones)
            {
                if (bitacora.IdAccion == accion.ID)
                {
                    bitacora.Accion = accion;
                }
            }

            foreach (Usuario usuario in catalogoUsuarios)
            {
                if (bitacora.IdUsuario == usuario.ID)
                {
                    bitacora.Usuario = usuario;
                }
            }

            ViewData["catalogoAcciones"] = catalogoAcciones;
            ViewBag.acciones = new SelectList(catalogoAcciones, "id", "nombre");

            ViewData["catalogoUsuarios"] = catalogoUsuarios;
            ViewBag.usuarios = new SelectList(catalogoUsuarios, "id", "nombre");

            ViewData["bitacora"] = bitacora;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Bitacora bitacora)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(bitacora, jsonSettings);


            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var putTask = client.PutAsync("bitacora/" + bitacora.ID, payloadContent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    bitacora = JsonConvert.DeserializeObject<Bitacora>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var bitacora = new Bitacora();
            var catalogoAcciones = new List<Accion>();
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Accion");
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

                    catalogoAcciones = JsonConvert.DeserializeObject<List<Accion>>(content.Result, jsonSettings);
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
                var responseTask = client.GetAsync("bitacora/" + ID);
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

                    bitacora = JsonConvert.DeserializeObject<Bitacora>(content.Result, jsonSettings);
                }
            }

            foreach (Accion accion in catalogoAcciones)
            {
                if (bitacora.IdAccion == accion.ID)
                {
                    bitacora.Accion = accion;
                }
            }

            foreach (Usuario usuario in catalogoUsuarios)
            {
                if (bitacora.IdUsuario == usuario.ID)
                {
                    bitacora.Usuario = usuario;
                }
            }

            ViewData["catalogoAcciones"] = catalogoAcciones;
            ViewBag.acciones = new SelectList(catalogoAcciones, "id", "nombre");

            ViewData["catalogoUsuarios"] = catalogoUsuarios;
            ViewBag.usuarios = new SelectList(catalogoUsuarios, "id", "nombre");

            ViewData["bitacora"] = bitacora;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Bitacora bitacora)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("bitacora/" + bitacora.ID);
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
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
    public class EscuelaController : Controller
    {
        public ActionResult Index() {
            var catalogoEscuelas = new List<Escuela>();
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
                var responseTask = client.GetAsync("Escuela");
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

                    catalogoEscuelas = JsonConvert.DeserializeObject<List<Escuela>>(content.Result, jsonSettings);
                }
            }

            foreach (Escuela escuela in catalogoEscuelas)
            {

                var newStatus = new Status();
                newStatus.Nombre = "No definido";
                escuela.Status = newStatus;

                foreach (Status status in catalogoStatuses)
                {
                    if (escuela.IdStatus == status.ID)
                    {
                        escuela.Status = status;
                    }
                }

                var newUbicacion = new Ubicacion();
                newUbicacion.Nombre = "No definido";
                escuela.Ubicacion = newUbicacion;

                foreach (Ubicacion ubicacion in catalogoUbicaciones)
                {
                    if (escuela.IdUbicacion == ubicacion.ID)
                    {
                        escuela.Ubicacion = ubicacion;
                    }
                }
            }

            ViewData["catalogoStatuses"] = catalogoStatuses;
            ViewBag.statuses = new SelectList(catalogoStatuses, "id", "nombre");

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            ViewBag.ubicaciones = new SelectList(catalogoUbicaciones, "id", "nombre");

            ViewData["catalogoEscuelas"] = catalogoEscuelas;
            return View();
        }

        public ActionResult Add() {
            var escuela = new Escuela();
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

            ViewData["catalogoStatuses"] = catalogoStatuses;
            ViewBag.statuses = new SelectList(catalogoStatuses, "id", "nombre");

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            ViewBag.ubicaciones = new SelectList(catalogoUbicaciones, "id", "nombre");

            ViewData["escuela"] = escuela;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Escuela escuela) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(escuela, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("escuela/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    escuela = JsonConvert.DeserializeObject<Escuela>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var escuela = new Escuela();
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
                var responseTask = client.GetAsync("escuela/" + ID);
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

                    escuela = JsonConvert.DeserializeObject<Escuela>(content.Result, jsonSettings);
                }
            }

            foreach (Status status in catalogoStatuses)
            {
                if (escuela.IdStatus == status.ID)
                {
                    escuela.Status = status;
                }
            }

            foreach (Ubicacion ubicacion in catalogoUbicaciones)
            {
                if (escuela.IdUbicacion == ubicacion.ID)
                {
                    escuela.Ubicacion = ubicacion;
                }
            }

            ViewData["catalogoStatuses"] = catalogoStatuses;
            ViewBag.statuses = new SelectList(catalogoStatuses, "id", "nombre");

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            ViewBag.ubicaciones = new SelectList(catalogoUbicaciones, "id", "nombre");

            ViewData["escuela"] = escuela;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Escuela escuela)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(escuela, jsonSettings);


            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var putTask = client.PutAsync("escuela/" + escuela.ID, payloadContent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    escuela = JsonConvert.DeserializeObject<Escuela>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var escuela = new Escuela();
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
                var responseTask = client.GetAsync("escuela/" + ID);
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

                    escuela = JsonConvert.DeserializeObject<Escuela>(content.Result, jsonSettings);
                }
            }

            foreach (Status status in catalogoStatuses)
            {
                if (escuela.IdStatus == status.ID)
                {
                    escuela.Status = status;
                }
            }

            foreach (Ubicacion ubicacion in catalogoUbicaciones)
            {
                if (escuela.IdUbicacion == ubicacion.ID)
                {
                    escuela.Ubicacion = ubicacion;
                }
            }

            ViewData["catalogoStatuses"] = catalogoStatuses;
            ViewBag.statuses = new SelectList(catalogoStatuses, "id", "nombre");

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            ViewBag.ubicaciones = new SelectList(catalogoUbicaciones, "id", "nombre");

            ViewData["escuela"] = escuela;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Escuela escuela)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("escuela/" + escuela.ID);
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
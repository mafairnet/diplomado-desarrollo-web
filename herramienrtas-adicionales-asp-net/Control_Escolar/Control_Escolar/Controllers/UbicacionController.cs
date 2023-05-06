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
    public class UbicacionController : Controller
    {
        public ActionResult Index() {
            var catalogoUbicaciones = new List<Ubicacion>();
            var catalogoCalles = new List<Calle>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Calle");
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

                    catalogoCalles = JsonConvert.DeserializeObject<List<Calle>>(content.Result, jsonSettings);
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

            foreach (Ubicacion ubicacion in catalogoUbicaciones)
            {

                var newCalle = new Calle();
                newCalle.Nombre = "No definido";
                ubicacion.Calle = newCalle;

                foreach (Calle calle in catalogoCalles)
                {
                    if (ubicacion.IdCalle == calle.ID)
                    {
                        ubicacion.Calle = calle;
                    }
                }
            }

            ViewData["catalogoCalles"] = catalogoCalles;
            ViewBag.calles = new SelectList(catalogoCalles, "id", "nombre");

            ViewData["catalogoUbicaciones"] = catalogoUbicaciones;
            return View();
        }

        public ActionResult Add() {
            var ubicacion = new Ubicacion();
            var catalogoCalles = new List<Calle>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Calle");
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

                    catalogoCalles = JsonConvert.DeserializeObject<List<Calle>>(content.Result, jsonSettings);
                }
            }

            ViewData["catalogoCalles"] = catalogoCalles;
            ViewBag.calles = new SelectList(catalogoCalles, "id", "nombre");

            ViewData["ubicacion"] = ubicacion;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Ubicacion ubicacion) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(ubicacion, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("ubicacion/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    ubicacion = JsonConvert.DeserializeObject<Ubicacion>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var ubicacion = new Ubicacion();
            var catalogoCalles = new List<Calle>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Calle");
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

                    catalogoCalles = JsonConvert.DeserializeObject<List<Calle>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("ubicacion/" + ID);
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

                    ubicacion = JsonConvert.DeserializeObject<Ubicacion>(content.Result, jsonSettings);
                }
            }

            foreach (Calle calle in catalogoCalles)
            {
                if (ubicacion.IdCalle == calle.ID)
                {
                    ubicacion.Calle = calle;
                }
            }

            ViewData["catalogoCalles"] = catalogoCalles;
            ViewBag.calles = new SelectList(catalogoCalles, "id", "nombre");

            ViewData["ubicacion"] = ubicacion;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Ubicacion ubicacion)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(ubicacion, jsonSettings);


            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var putTask = client.PutAsync("ubicacion/" + ubicacion.ID, payloadContent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    ubicacion = JsonConvert.DeserializeObject<Ubicacion>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var ubicacion = new Ubicacion();
            var catalogoCalles = new List<Calle>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Calle");
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

                    catalogoCalles = JsonConvert.DeserializeObject<List<Calle>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("ubicacion/" + ID);
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

                    ubicacion = JsonConvert.DeserializeObject<Ubicacion>(content.Result, jsonSettings);
                }
            }

            foreach (Calle calle in catalogoCalles)
            {
                if (ubicacion.IdCalle == calle.ID)
                {
                    ubicacion.Calle = calle;
                }
            }

            ViewData["catalogoCalles"] = catalogoCalles;
            ViewBag.calles = new SelectList(catalogoCalles, "id", "nombre");

            ViewData["ubicacion"] = ubicacion;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Ubicacion ubicacion)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("ubicacion/" + ubicacion.ID);
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
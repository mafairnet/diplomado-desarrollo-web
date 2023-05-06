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
    public class CalleController : Controller
    {
        public ActionResult Index() {
            var catalogoCalles = new List<Calle>();
            var catalogoLocalidades = new List<Localidad>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Localidad");
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

                    catalogoLocalidades = JsonConvert.DeserializeObject<List<Localidad>>(content.Result, jsonSettings);
                }
            }

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

            foreach (Calle calle in catalogoCalles)
            {

                var newLocalidad = new Localidad();
                newLocalidad.Nombre = "No definido";
                calle.Localidad = newLocalidad;

                foreach (Localidad localidad in catalogoLocalidades)
                {
                    if (calle.IdLocalidad == localidad.ID)
                    {
                        calle.Localidad = localidad;
                    }
                }
            }

            ViewData["catalogoLocalidades"] = catalogoLocalidades;
            ViewBag.localidades = new SelectList(catalogoLocalidades, "id", "nombre");

            ViewData["catalogoCalles"] = catalogoCalles;
            return View();
        }

        public ActionResult Add() {
            var calle = new Calle();
            var catalogoLocalidades = new List<Localidad>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Localidad");
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

                    catalogoLocalidades = JsonConvert.DeserializeObject<List<Localidad>>(content.Result, jsonSettings);
                }
            }

            ViewData["catalogoLocalidades"] = catalogoLocalidades;
            ViewBag.localidades = new SelectList(catalogoLocalidades, "id", "nombre");

            ViewData["calle"] = calle;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Calle calle) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(calle, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("calle/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    calle = JsonConvert.DeserializeObject<Calle>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var calle = new Calle();
            var catalogoLocalidades = new List<Localidad>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Localidad");
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

                    catalogoLocalidades = JsonConvert.DeserializeObject<List<Localidad>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("calle/" + ID);
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

                    calle = JsonConvert.DeserializeObject<Calle>(content.Result, jsonSettings);
                }
            }

            foreach (Localidad localidad in catalogoLocalidades)
            {
                if (calle.IdLocalidad == localidad.ID)
                {
                    calle.Localidad = localidad;
                }
            }

            ViewData["catalogoLocalidades"] = catalogoLocalidades;
            ViewBag.localidades = new SelectList(catalogoLocalidades, "id", "nombre");

            ViewData["calle"] = calle;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Calle calle)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(calle, jsonSettings);


            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var putTask = client.PutAsync("calle/" + calle.ID, payloadContent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    calle = JsonConvert.DeserializeObject<Calle>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var calle = new Calle();
            var catalogoLocalidades = new List<Localidad>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Localidad");
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

                    catalogoLocalidades = JsonConvert.DeserializeObject<List<Localidad>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("calle/" + ID);
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

                    calle = JsonConvert.DeserializeObject<Calle>(content.Result, jsonSettings);
                }
            }

            foreach (Localidad localidad in catalogoLocalidades)
            {
                if (calle.IdLocalidad == localidad.ID)
                {
                    calle.Localidad = localidad;
                }
            }

            ViewData["catalogoLocalidades"] = catalogoLocalidades;
            ViewBag.localidades = new SelectList(catalogoLocalidades, "id", "nombre");

            ViewData["calle"] = calle;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Calle calle)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("calle/" + calle.ID);
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
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
    public class EstadoController : Controller
    {
        public ActionResult Index() {
            var catalogoEstados = new List<Estado>();
            var catalogoPaises = new List<Pais>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Pais");
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

                    catalogoPaises = JsonConvert.DeserializeObject<List<Pais>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Estado");
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

                    catalogoEstados = JsonConvert.DeserializeObject<List<Estado>>(content.Result, jsonSettings);
                }
            }

            foreach (Estado estado in catalogoEstados)
            {

                var newPais = new Pais();
                newPais.Nombre = "No definido";
                estado.Pais = newPais;

                foreach (Pais pais in catalogoPaises)
                {
                    if (estado.IdPais == pais.ID)
                    {
                        estado.Pais = pais;
                    }
                }
            }

            ViewData["catalogoPaises"] = catalogoPaises;
            ViewBag.paises = new SelectList(catalogoPaises, "id", "nombre");

            ViewData["catalogoEstados"] = catalogoEstados;
            return View();
        }

        public ActionResult Add() {
            var estado = new Estado();
            var catalogoPaises = new List<Pais>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Pais");
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

                    catalogoPaises = JsonConvert.DeserializeObject<List<Pais>>(content.Result, jsonSettings);
                }
            }

            foreach (Pais pais in catalogoPaises)
            {
                if (estado.IdPais == pais.ID)
                {
                    estado.Pais = pais;
                }
            }

            ViewData["catalogoPaises"] = catalogoPaises;
            ViewBag.paises = new SelectList(catalogoPaises, "id", "nombre");

            ViewData["estado"] = estado;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Estado estado) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(estado, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("estado/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    estado = JsonConvert.DeserializeObject<Estado>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var estado = new Estado();
            var catalogoPaises = new List<Pais>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Pais");
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

                    catalogoPaises = JsonConvert.DeserializeObject<List<Pais>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("estado/" + ID);
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

                    estado = JsonConvert.DeserializeObject<Estado>(content.Result, jsonSettings);
                }
            }

            foreach (Pais pais in catalogoPaises)
            {
                if (estado.IdPais == pais.ID)
                {
                    estado.Pais = pais;
                }
            }

            ViewData["catalogoPaises"] = catalogoPaises;
            ViewBag.paises = new SelectList(catalogoPaises, "id", "nombre");

            ViewData["estado"] = estado;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Estado estado)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(estado, jsonSettings);


            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var putTask = client.PutAsync("estado/" + estado.ID, payloadContent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    estado = JsonConvert.DeserializeObject<Estado>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var estado = new Estado();
            var catalogoPaises = new List<Pais>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Pais");
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

                    catalogoPaises = JsonConvert.DeserializeObject<List<Pais>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("estado/" + ID);
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

                    estado = JsonConvert.DeserializeObject<Estado>(content.Result, jsonSettings);
                }
            }

            foreach (Pais pais in catalogoPaises)
            {
                if (estado.IdPais == pais.ID)
                {
                    estado.Pais = pais;
                }
            }

            ViewData["catalogoPaises"] = catalogoPaises;
            ViewBag.paises = new SelectList(catalogoPaises, "id", "nombre");

            ViewData["estado"] = estado;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Estado estado)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("estado/" + estado.ID);
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
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
    public class MunicipioController : Controller
    {
        public ActionResult Index() {
            var catalogoMunicipios = new List<Municipio>();
            var catalogoEstados = new List<Estado>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Municipio");
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

                    catalogoMunicipios = JsonConvert.DeserializeObject<List<Municipio>>(content.Result, jsonSettings);
                }
            }

            foreach (Municipio municipio in catalogoMunicipios)
            {

                var newEstado = new Estado();
                newEstado.Nombre = "No definido";
                municipio.Estado = newEstado;

                foreach (Estado estado in catalogoEstados)
                {
                    if (municipio.IdEstado == estado.ID)
                    {
                        municipio.Estado = estado;
                    }
                }
            }

            ViewData["catalogoEstados"] = catalogoEstados;
            ViewBag.estados = new SelectList(catalogoEstados, "id", "nombre");

            ViewData["catalogoMunicipios"] = catalogoMunicipios;
            return View();
        }

        public ActionResult Add() {
            var municipio = new Municipio();

            var catalogoEstados = new List<Estado>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

            ViewData["catalogoEstados"] = catalogoEstados;
            ViewBag.estados = new SelectList(catalogoEstados, "id", "nombre");

            ViewData["municipio"] = municipio;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Municipio municipio) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(municipio, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("municipio/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    municipio = JsonConvert.DeserializeObject<Municipio>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var municipio = new Municipio();
            var catalogoEstados = new List<Estado>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("municipio/" + ID);
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

                    municipio = JsonConvert.DeserializeObject<Municipio>(content.Result, jsonSettings);
                }
            }

            foreach (Estado estado in catalogoEstados)
            {
                if (municipio.IdEstado == municipio.ID)
                {
                    municipio.Estado = estado;
                }
            }

            ViewData["catalogoEstados"] = catalogoEstados;
            ViewBag.estados = new SelectList(catalogoEstados, "id", "nombre");

            ViewData["municipio"] = municipio;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Municipio municipio)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(municipio, jsonSettings);


            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var putTask = client.PutAsync("municipio/" + municipio.ID, payloadContent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    municipio = JsonConvert.DeserializeObject<Municipio>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var municipio = new Municipio();
            var catalogoEstados = new List<Estado>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("municipio/" + ID);
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

                    municipio = JsonConvert.DeserializeObject<Municipio>(content.Result, jsonSettings);
                }
            }

            foreach (Estado estado in catalogoEstados)
            {
                if (municipio.IdEstado == municipio.ID)
                {
                    municipio.Estado = estado;
                }
            }

            ViewData["catalogoEstados"] = catalogoEstados;
            ViewBag.estados = new SelectList(catalogoEstados, "id", "nombre");

            ViewData["municipio"] = municipio;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Municipio municipio)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("municipio/" + municipio.ID);
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
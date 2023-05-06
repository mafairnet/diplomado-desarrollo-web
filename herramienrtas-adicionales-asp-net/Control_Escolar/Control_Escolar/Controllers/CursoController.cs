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
    public class CursoController : Controller
    {
        public ActionResult Index() {
            var catalogoCursos = new List<Curso>();
            var catalogoPeriodos = new List<Periodo>();
            var catalogoAsignaturas = new List<Asignatura>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Periodo");
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

                    catalogoPeriodos = JsonConvert.DeserializeObject<List<Periodo>>(content.Result, jsonSettings);
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
                var responseTask = client.GetAsync("Curso");
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

                    catalogoCursos = JsonConvert.DeserializeObject<List<Curso>>(content.Result, jsonSettings);
                }
            }

            foreach (Curso curso in catalogoCursos)
            {

                var newPeriodo = new Periodo();
                newPeriodo.Nombre = "No definido";
                curso.Periodo = newPeriodo;

                foreach (Periodo periodo in catalogoPeriodos)
                {
                    if (curso.IdPeriodo == periodo.ID)
                    {
                        curso.Periodo = periodo;
                    }
                }

                var newAsignatura = new Asignatura();
                newAsignatura.Nombre = "No definido";
                curso.Asignatura = newAsignatura;

                foreach (Asignatura asignatura in catalogoAsignaturas)
                {
                    if (curso.IdAsignatura == asignatura.ID)
                    {
                        curso.Asignatura = asignatura;
                    }
                }
            }

            ViewData["catalogoPeriodos"] = catalogoPeriodos;
            ViewBag.periodos = new SelectList(catalogoPeriodos, "id", "nombre");

            ViewData["catalogoAsignaturas"] = catalogoAsignaturas;
            ViewBag.asignaturas = new SelectList(catalogoAsignaturas, "id", "nombre");

            ViewData["catalogoCursos"] = catalogoCursos;
            return View();
        }

        public ActionResult Add() {
            var curso = new Curso();
            var catalogoPeriodos = new List<Periodo>();
            var catalogoAsignaturas = new List<Asignatura>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Periodo");
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

                    catalogoPeriodos = JsonConvert.DeserializeObject<List<Periodo>>(content.Result, jsonSettings);
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

            ViewData["catalogoPeriodos"] = catalogoPeriodos;
            ViewBag.periodoes = new SelectList(catalogoPeriodos, "id", "nombre");

            ViewData["catalogoAsignaturas"] = catalogoAsignaturas;
            ViewBag.asignaturas = new SelectList(catalogoAsignaturas, "id", "nombre");

            ViewData["curso"] = curso;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Curso curso) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(curso, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("curso/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    curso = JsonConvert.DeserializeObject<Curso>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var curso = new Curso();
            var catalogoPeriodos = new List<Periodo>();
            var catalogoAsignaturas = new List<Asignatura>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Periodo");
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

                    catalogoPeriodos = JsonConvert.DeserializeObject<List<Periodo>>(content.Result, jsonSettings);
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
                var responseTask = client.GetAsync("curso/" + ID);
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

                    curso = JsonConvert.DeserializeObject<Curso>(content.Result, jsonSettings);
                }
            }

            foreach (Periodo periodo in catalogoPeriodos)
            {
                if (curso.IdPeriodo == periodo.ID)
                {
                    curso.Periodo = periodo;
                }
            }

            foreach (Asignatura asignatura in catalogoAsignaturas)
            {
                if (curso.IdAsignatura == asignatura.ID)
                {
                    curso.Asignatura = asignatura;
                }
            }

            ViewData["catalogoPeriodos"] = catalogoPeriodos;
            ViewBag.periodoes = new SelectList(catalogoPeriodos, "id", "nombre");

            ViewData["catalogoAsignaturas"] = catalogoAsignaturas;
            ViewBag.asignaturas = new SelectList(catalogoAsignaturas, "id", "nombre");

            ViewData["curso"] = curso;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Curso curso)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(curso, jsonSettings);


            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var putTask = client.PutAsync("curso/" + curso.ID, payloadContent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    curso = JsonConvert.DeserializeObject<Curso>(content.Result, jsonSettings);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var curso = new Curso();
            var catalogoPeriodos = new List<Periodo>();
            var catalogoAsignaturas = new List<Asignatura>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("Periodo");
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

                    catalogoPeriodos = JsonConvert.DeserializeObject<List<Periodo>>(content.Result, jsonSettings);
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
                var responseTask = client.GetAsync("curso/" + ID);
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

                    curso = JsonConvert.DeserializeObject<Curso>(content.Result, jsonSettings);
                }
            }

            foreach (Periodo periodo in catalogoPeriodos)
            {
                if (curso.IdPeriodo == periodo.ID)
                {
                    curso.Periodo = periodo;
                }
            }

            foreach (Asignatura asignatura in catalogoAsignaturas)
            {
                if (curso.IdAsignatura == asignatura.ID)
                {
                    curso.Asignatura = asignatura;
                }
            }

            ViewData["catalogoPeriodos"] = catalogoPeriodos;
            ViewBag.periodoes = new SelectList(catalogoPeriodos, "id", "nombre");

            ViewData["catalogoAsignaturas"] = catalogoAsignaturas;
            ViewBag.asignaturas = new SelectList(catalogoAsignaturas, "id", "nombre");

            ViewData["curso"] = curso;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Curso curso)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("curso/" + curso.ID);
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
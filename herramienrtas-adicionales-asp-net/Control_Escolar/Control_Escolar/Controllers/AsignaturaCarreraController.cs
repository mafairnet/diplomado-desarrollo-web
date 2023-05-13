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
    public class AsignaturaCarreraController : Controller
    {
        public ActionResult Index(int ID) {

            var catalogoAsignaturas = new List<Asignatura>();
            var catalogoCarreras = new List<Carrera>();
            var catalogoStatus = new List<Status>();
            var catalogoAsignaturaCarreras = new List<AsignaturaCarrera>();
            

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
                var responseTask = client.GetAsync("AsignaturaCarrera");
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

                    catalogoAsignaturaCarreras = JsonConvert.DeserializeObject<List<AsignaturaCarrera>>(content.Result, jsonSettings);
                }
            }

            var carrera = catalogoCarreras.Where(x => x.ID == ID).First();

            var asignaturasCarreras = new List<Asignatura>();

            foreach (AsignaturaCarrera asignaturaCarrera in catalogoAsignaturaCarreras)
            {
                foreach (Asignatura asignatura in catalogoAsignaturas)
                {
                    if (asignaturaCarrera.IdAsignatura == asignatura.ID && asignaturaCarrera.IdCarrera == carrera.ID)
                    {
                        foreach (Status status in catalogoStatus) {
                            if (asignatura.IdStatus == status.ID) {
                                asignatura.Status = status;
                            }
                        }
                        asignaturasCarreras.Add(asignatura);
                    }
                }
            }
            carrera.Asignaturas = asignaturasCarreras;

            ViewData["carrera"] = carrera;
            return View();
        }

        public ActionResult Add(int ID) {
            var asignaturaCarrera = new AsignaturaCarrera();
            var catalogoCarreras = new List<Carrera>();
            var catalogoAsignaturas = new List<Asignatura>();
            var catalogoAsignaturaCarreras = new List<AsignaturaCarrera>();

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
                var responseTask = client.GetAsync("AsignaturaCarrera");
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

                    catalogoAsignaturaCarreras = JsonConvert.DeserializeObject<List<AsignaturaCarrera>>(content.Result, jsonSettings);
                }
            }

            var catalogoAsignaturaNoAsignadas = new List<Asignatura>();

            foreach (Asignatura asignatura in catalogoAsignaturas)
            {
                var asignada = false;
                foreach (AsignaturaCarrera asignaturaCarreraItem in catalogoAsignaturaCarreras)
                {
                    if (asignatura.ID == asignaturaCarreraItem.IdAsignatura && asignaturaCarreraItem.IdCarrera == ID)
                    {
                        asignada = true;
                    }
                }
                if (!asignada) {
                    catalogoAsignaturaNoAsignadas.Add(asignatura);
                }
            }


            var carrera = catalogoCarreras.Where(x => x.ID == ID).First();

            ViewData["catalogoAsignaturas"] = catalogoAsignaturaNoAsignadas;
            ViewBag.asignaturas = new SelectList(catalogoAsignaturaNoAsignadas, "id", "nombre");

            asignaturaCarrera.IdCarrera = ID;

            ViewData["asignaturaCarrera"] = asignaturaCarrera;
            ViewData["carrera"] = carrera;
            return View();
        }

        [HttpPost]
        public ActionResult Add(AsignaturaCarrera asignaturaCarrera) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(asignaturaCarrera, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("asignaturaCarrera/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    asignaturaCarrera = JsonConvert.DeserializeObject<AsignaturaCarrera>(content.Result, jsonSettings);
                }
            }
            
            return RedirectToAction("Index/" + asignaturaCarrera.IdCarrera);
        }

        
        public ActionResult Delete(String ID)
        {
            String[] idArray = ID.Split('_');

            int idAsignatura = int.Parse(idArray[0]);
            int idCarrera = int.Parse(idArray[1]);

            var asignaturaCarrera = new AsignaturaCarrera();
            var catalogoCarreras = new List<Carrera>();
            var catalogoAsignaturas = new List<Asignatura>();

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

            var asignatura = catalogoAsignaturas.Where(x => x.ID == idAsignatura).First();
            var carrera = catalogoCarreras.Where(x => x.ID == idCarrera).First();

            ViewData["catalogoAsignaturas"] = catalogoAsignaturas;
            ViewBag.asignaturas = new SelectList(catalogoAsignaturas, "id", "nombre");

            asignaturaCarrera.IdAsignatura = idAsignatura;
            asignaturaCarrera.IdCarrera = idCarrera;


            ViewData["asignaturaCarrera"] = asignaturaCarrera;
            ViewData["carrera"] = carrera;

            return View();
        }

        [HttpPost]
        public ActionResult Delete(AsignaturaCarrera asignaturaCarrera)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("asignaturaCarrera/" + asignaturaCarrera.IdAsignatura + "_" + asignaturaCarrera.IdCarrera);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                }
            }
            return RedirectToAction("Index/" + asignaturaCarrera.IdCarrera);
        }

    }
}
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
    public class CarreraEscuelaController : Controller
    {
        public ActionResult Index(int ID) {

            var catalogoEscuelas = new List<Escuela>();
            var catalogoCarreras = new List<Carrera>();
            var catalogoStatus = new List<Status>();
            var catalogoCarreraEscuelas = new List<CarreraEscuela>();
            

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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
                var responseTask = client.GetAsync("CarreraEscuela");
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

                    catalogoCarreraEscuelas = JsonConvert.DeserializeObject<List<CarreraEscuela>>(content.Result, jsonSettings);
                }
            }

            var escuela = catalogoEscuelas.Where(x => x.ID == ID).First();

            var carrerasEscuela = new List<Carrera>();

            foreach (CarreraEscuela carreraEscuela in catalogoCarreraEscuelas)
            {
                foreach (Carrera carrera in catalogoCarreras)
                {
                    if (carreraEscuela.IdEscuela == escuela.ID && carreraEscuela.IdCarrera == carrera.ID)
                    {
                        foreach (Status status in catalogoStatus) {
                            if (carrera.IdStatus == status.ID) {
                                carrera.Status = status;
                            }
                        }
                        carrerasEscuela.Add(carrera);
                    }
                }
            }
            escuela.Carreras = carrerasEscuela;

            ViewData["escuela"] = escuela;
            return View();
        }

        public ActionResult Add(int ID) {
            var carreraEscuela = new CarreraEscuela();
            var catalogoCarreras = new List<Carrera>();
            var catalogoEscuelas = new List<Escuela>();
            var catalogoCarreraEscuelas = new List<CarreraEscuela>();

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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("CarreraEscuela");
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

                    catalogoCarreraEscuelas = JsonConvert.DeserializeObject<List<CarreraEscuela>>(content.Result, jsonSettings);
                }
            }

            var catalogoCarrerasNoAsignadas = new List<Carrera>();

            foreach (Carrera carrera in catalogoCarreras)
            {
                var asignada = false;
                foreach (CarreraEscuela carreraEscuelaItem in catalogoCarreraEscuelas)
                {
                    if (carrera.ID == carreraEscuelaItem.IdCarrera && carreraEscuelaItem.IdEscuela == ID)
                    {
                        asignada = true;
                    }
                }
                if (!asignada) {
                    catalogoCarrerasNoAsignadas.Add(carrera);
                }
            }


            var escuela = catalogoEscuelas.Where(x => x.ID == ID).First();

            ViewData["catalogoCarreras"] = catalogoCarrerasNoAsignadas;
            ViewBag.carreras = new SelectList(catalogoCarrerasNoAsignadas, "id", "nombre");

            carreraEscuela.IdEscuela = ID;

            ViewData["carreraEscuela"] = carreraEscuela;
            ViewData["escuela"] = escuela;
            return View();
        }

        [HttpPost]
        public ActionResult Add(CarreraEscuela carreraEscuela) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(carreraEscuela, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("carreraEscuela/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    carreraEscuela = JsonConvert.DeserializeObject<CarreraEscuela>(content.Result, jsonSettings);
                }
            }
            
            return RedirectToAction("Index/" + carreraEscuela.IdEscuela);
        }

        
        public ActionResult Delete(String ID)
        {
            String[] idArray = ID.Split('_');

            int idEscuela = int.Parse(idArray[0]);
            int idCarrera = int.Parse(idArray[1]);

            var carreraEscuela = new CarreraEscuela();
            var catalogoCarreras = new List<Carrera>();
            var catalogoEscuelas = new List<Escuela>();

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

            var escuela = catalogoEscuelas.Where(x => x.ID == idEscuela).First();
            var carrera = catalogoCarreras.Where(x => x.ID == idCarrera).First();

            ViewData["catalogoCarreras"] = catalogoCarreras;
            ViewBag.carreras = new SelectList(catalogoCarreras, "id", "nombre");

            carreraEscuela.IdEscuela = idEscuela;
            carreraEscuela.IdCarrera = idCarrera;


            ViewData["carreraEscuela"] = carreraEscuela;
            ViewData["escuela"] = escuela;

            return View();
        }

        [HttpPost]
        public ActionResult Delete(CarreraEscuela carreraEscuela)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("carreraEscuela/" + carreraEscuela.IdEscuela + "_" + carreraEscuela.IdCarrera);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                }
            }
            return RedirectToAction("Index/" + carreraEscuela.IdEscuela);
        }

    }
}
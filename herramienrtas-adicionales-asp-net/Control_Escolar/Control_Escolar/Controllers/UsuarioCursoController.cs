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
    public class UsuarioCursoController : Controller
    {
        public ActionResult Index(int ID) {

            var catalogoUsuarios = new List<Usuario>();
            var catalogoCursos = new List<Curso>();
            var catalogoUsuarioCursos = new List<UsuarioCurso>();
            

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("UsuarioCurso");
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

                    catalogoUsuarioCursos = JsonConvert.DeserializeObject<List<UsuarioCurso>>(content.Result, jsonSettings);
                }
            }

            var usuario = catalogoUsuarios.Where(x => x.ID == ID).First();

            var cursosUsuario = new List<Curso>();

            foreach (UsuarioCurso usuarioCurso in catalogoUsuarioCursos)
            {
                foreach (Curso curso in catalogoCursos)
                {
                    if (usuarioCurso.IdUsuario == usuario.ID && usuarioCurso.IdCurso == curso.ID)
                    {
                        cursosUsuario.Add(curso);
                    }
                }
            }
            usuario.Cursos = cursosUsuario;

            ViewData["usuario"] = usuario;
            return View();
        }

        public ActionResult Add(int ID) {
            var usuarioCurso = new UsuarioCurso();
            var catalogoCursos = new List<Curso>();
            var catalogoUsuarios = new List<Usuario>();
            var catalogoUsuarioCursos = new List<UsuarioCurso>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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
                var responseTask = client.GetAsync("UsuarioCurso");
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

                    catalogoUsuarioCursos = JsonConvert.DeserializeObject<List<UsuarioCurso>>(content.Result, jsonSettings);
                }
            }

            var catalogoCursosNoAsignadas = new List<Curso>();

            foreach (Curso curso in catalogoCursos)
            {
                var asignada = false;
                foreach (UsuarioCurso usuarioCursoItem in catalogoUsuarioCursos)
                {
                    if (curso.ID == usuarioCursoItem.IdCurso && usuarioCursoItem.IdUsuario == ID)
                    {
                        asignada = true;
                    }
                }
                if (!asignada) {
                    catalogoCursosNoAsignadas.Add(curso);
                }
            }


            var usuario = catalogoUsuarios.Where(x => x.ID == ID).First();

            ViewData["catalogoCursos"] = catalogoCursosNoAsignadas;
            ViewBag.cursos = new SelectList(catalogoCursosNoAsignadas, "id", "nombre");

            usuarioCurso.IdUsuario = ID;

            ViewData["usuarioCurso"] = usuarioCurso;
            ViewData["usuario"] = usuario;
            return View();
        }

        [HttpPost]
        public ActionResult Add(UsuarioCurso usuarioCurso) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(usuarioCurso, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("usuarioCurso/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    usuarioCurso = JsonConvert.DeserializeObject<UsuarioCurso>(content.Result, jsonSettings);
                }
            }
            
            return RedirectToAction("Index/" + usuarioCurso.IdUsuario);
        }

        
        public ActionResult Delete(String ID)
        {
            String[] idArray = ID.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int idCurso = int.Parse(idArray[1]);

            var usuarioCurso = new UsuarioCurso();
            var catalogoCursos = new List<Curso>();
            var catalogoUsuarios = new List<Usuario>();

            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

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

            var usuario = catalogoUsuarios.Where(x => x.ID == idUsuario).First();
            var curso = catalogoCursos.Where(x => x.ID == idCurso).First();

            ViewData["catalogoCursos"] = catalogoCursos;
            ViewBag.cursos = new SelectList(catalogoCursos, "id", "nombre");

            usuarioCurso.IdUsuario = idUsuario;
            usuarioCurso.IdCurso = idCurso;


            ViewData["usuarioCurso"] = usuarioCurso;
            ViewData["usuario"] = usuario;

            return View();
        }

        [HttpPost]
        public ActionResult Delete(UsuarioCurso usuarioCurso)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("usuarioCurso/" + usuarioCurso.IdUsuario + "_" + usuarioCurso.IdCurso);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                }
            }
            return RedirectToAction("Index/" + usuarioCurso.IdUsuario);
        }

    }
}
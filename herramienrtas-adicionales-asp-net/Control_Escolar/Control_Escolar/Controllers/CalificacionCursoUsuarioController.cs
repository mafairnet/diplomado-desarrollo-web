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
    public class CalificacionCursoUsuarioController : Controller
    {
        public ActionResult Index(int ID) {

            var catalogoUsuarios = new List<Usuario>();
            var catalogoCursos = new List<Curso>();
            var catalogoCalificaciones = new List<Calificacion>();

            var catalogoUsuarioCursos = new List<UsuarioCurso>();
            var catalogoCalificacionCursoUsuarios = new List<CalificacionCursoUsuario>();
            

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
                var responseTask = client.GetAsync("Calificacion");
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

                    catalogoCalificaciones = JsonConvert.DeserializeObject<List<Calificacion>>(content.Result, jsonSettings);
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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("CalificacionCursoUsuario");
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

                    catalogoCalificacionCursoUsuarios = JsonConvert.DeserializeObject<List<CalificacionCursoUsuario>>(content.Result, jsonSettings);
                }
            }

            var cursoActual = catalogoCursos.Where(x => x.ID == ID).First();
            var usuariosCurso = new List<Usuario>();

            foreach (Usuario usuario in catalogoUsuarios)
            {
                var asignado = false;
                foreach (UsuarioCurso usuarioCursoItem in catalogoUsuarioCursos)
                {
                    if (cursoActual.ID == usuarioCursoItem.IdCurso && usuarioCursoItem.IdUsuario == ID)
                    {
                        asignado = true;
                    }
                }
                if (asignado)
                {
                    var calificacionAsignada = new Calificacion();
                    calificacionAsignada.Valor = "No Asignada";
                    usuario.CalificacionCurso = calificacionAsignada;
                    usuariosCurso.Add(usuario);
                }
            }



            foreach (CalificacionCursoUsuario calificacionCursoUsuario in catalogoCalificacionCursoUsuarios)
            {
                foreach (Usuario usuario in usuariosCurso)
                {
                    if (calificacionCursoUsuario.IdUsuario == usuario.ID && calificacionCursoUsuario.IdCurso == cursoActual.ID)
                    {
                        var calificacionAsignada = new Calificacion();
                        calificacionAsignada.Valor = "No Asignada";
                        foreach (Calificacion calificacion in catalogoCalificaciones)
                        {
                            if (calificacionCursoUsuario.IdUsuario == usuario.ID && calificacionCursoUsuario.IdCalificacion == calificacion.ID)
                            {
                                calificacionAsignada = calificacion;
                            }
                        }
                        usuario.CalificacionCurso = calificacionAsignada;
                    }
                }
                
            }
            cursoActual.Usuarios = usuariosCurso;

            ViewData["curso"] = cursoActual;
            return View();
        }

        public ActionResult Add(String ID) {

            String[] idArray = ID.Split('_');

            
            int idCurso = int.Parse(idArray[0]);
            int idUsuario = int.Parse(idArray[1]);

            var calificacionCursoUsuario = new CalificacionCursoUsuario();

            var catalogoCursos = new List<Curso>();
            var catalogoUsuarios = new List<Usuario>();
            var catalogoCalificaciones = new List<Calificacion>();

            var catalogoCalificacionCursoUsuarios = new List<CalificacionCursoUsuario>();

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
                var responseTask = client.GetAsync("Calificacion");
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

                    catalogoCalificaciones = JsonConvert.DeserializeObject<List<Calificacion>>(content.Result, jsonSettings);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.GetAsync("CalificacionCursoUsuario");
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

                    catalogoCalificacionCursoUsuarios = JsonConvert.DeserializeObject<List<CalificacionCursoUsuario>>(content.Result, jsonSettings);
                }
            }

            
            var curso = catalogoCursos.Where(x => x.ID == idCurso).First();
            var usuario = catalogoUsuarios.Where(x => x.ID == idUsuario).First();

            ViewData["catalogoCalificaciones"] = catalogoCalificaciones;
            ViewBag.calificaciones = new SelectList(catalogoCalificaciones, "id", "valor");

            calificacionCursoUsuario.IdCurso = idCurso;
            calificacionCursoUsuario.IdUsuario = idUsuario;

            ViewData["calificacionCursoUsuario"] = calificacionCursoUsuario;

            ViewData["curso"] = curso;
            ViewData["usuario"] = usuario;
            return View();
        }

        [HttpPost]
        public ActionResult Add(CalificacionCursoUsuario calificacionCursoUsuario) {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var payload = JsonConvert.SerializeObject(calificacionCursoUsuario, jsonSettings);

            var payloadContent = new StringContent(payload, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                var responseTask = client.PostAsync("calificacionCursoUsuario/", payloadContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();

                    calificacionCursoUsuario = JsonConvert.DeserializeObject<CalificacionCursoUsuario>(content.Result, jsonSettings);
                }
            }
            
            return RedirectToAction("Index/" + calificacionCursoUsuario.IdCurso);
        }

        
        public ActionResult Delete(String ID)
        {
            String[] idArray = ID.Split('_');

            int idUsuario = int.Parse(idArray[0]);
            int idCurso = int.Parse(idArray[1]);
            int idCalificacion = int.Parse(idArray[2]);

            var calificacionCursoUsuario = new CalificacionCursoUsuario();

            var catalogoCursos = new List<Curso>();
            var catalogoUsuarios = new List<Usuario>();
            var catalogoCalificaciones = new List<Calificacion>();


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
                var responseTask = client.GetAsync("Calificacion");
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

                    catalogoCalificaciones = JsonConvert.DeserializeObject<List<Calificacion>>(content.Result, jsonSettings);
                }
            }

            var usuario = catalogoUsuarios.Where(x => x.ID == idUsuario).First();
            var curso = catalogoCursos.Where(x => x.ID == idCurso).First();
            var calificacion = catalogoCalificaciones.Where(x => x.ID == idCalificacion).First();

            ViewData["catalogoCursos"] = catalogoCursos;
            ViewBag.calificaciones = new SelectList(catalogoCalificaciones, "id", "valor");

            calificacionCursoUsuario.IdUsuario = idUsuario;
            calificacionCursoUsuario.IdCurso = idCurso;
            calificacionCursoUsuario.IdCalificacion = idCalificacion;


            ViewData["calificacionCursoUsuario"] = calificacionCursoUsuario;
            ViewData["curso"] = curso;
            ViewData["usuario"] = usuario;

            return View();
        }

        [HttpPost]
        public ActionResult Delete(CalificacionCursoUsuario calificacionCursoUsuario)
        {
            var apiURL = WebConfigurationManager.AppSettings["ControlEscolarApiUri"];
            var apiKey = WebConfigurationManager.AppSettings["ControlEscolarApiKey"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                var deleteTask = client.DeleteAsync("calificacionCursoUsuario/" + calificacionCursoUsuario.IdCurso + "_" + calificacionCursoUsuario.IdUsuario + "_" + calificacionCursoUsuario.IdCalificacion);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                }
            }
            return RedirectToAction("Index/" + calificacionCursoUsuario.IdCurso);
        }

    }
}
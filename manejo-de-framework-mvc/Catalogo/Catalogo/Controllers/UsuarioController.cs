using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Catalogo.Dto;
using Catalogo.Dao;
using System.Data;
using Catalogo.Models;
using Microsoft.Ajax.Utilities;

namespace Catalogo.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            //var usuariosActuales = new List<string>();
            var usuariosActuales = new List<Usuario>();
            DTOUsuario dto = new DTOUsuario();
            DAOUsuario dao = new DAOUsuario();

            //dto.Id = 3;

            /*
            DataSet ds;

            //dto.Id = 3;

            ds = dao.GetUsuario(dto);

            if (ds != null) {
                foreach (DataTable table in ds.Tables) {
                    foreach (DataRow row in table.Rows) {
                        var usuarioActual = "";
                        //System.Diagnostics.Debug.WriteLine(row["id"].ToString());
                        //System.Diagnostics.Debug.WriteLine(row["first_name"].ToString());
                        //System.Diagnostics.Debug.WriteLine(row["second_name"].ToString());
                        //System.Diagnostics.Debug.WriteLine(row["username"].ToString());
                        //System.Diagnostics.Debug.WriteLine(row["password"].ToString());
                        usuarioActual = row["id"].ToString() + "," + row["first_name"].ToString() + "," + row["second_name"].ToString()  + "," + row["username"].ToString() + "," + row["password"].ToString();
                        //usuariosActuales += usuarioActual;
                        usuariosActuales.Add(usuarioActual);
                    }
                    
                }
            }*/

            usuariosActuales = dao.GetUsuarios(dto);


            //ViewBag.Messagge = usuariosACtuales;

            //ViewBag.usuariosACtuales = usuariosACtuales;
            ViewData["usuariosActuales"] = usuariosActuales;

            return View();
        }

        public ActionResult Add()
        {

            var usuario = new Usuario();

            ViewData["usuario"] = usuario;

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Add(Usuario usuario)
        {
            DTOUsuario dto = new DTOUsuario();
            DAOUsuario dao = new DAOUsuario();

            dto.FirstName = usuario.FirstName;
            dto.SecondName = usuario.SecondName;
            dto.UserName = usuario.Username;
            dto.Password = usuario.Password;

            var resultado = dao.AddUsuario(dto);

            return RedirectToAction("Index");
        }

        //Peticion GET de Usuario por ID
        public ActionResult Edit(int ID)
        {

            var usuario = new Usuario();

            DTOUsuario dto = new DTOUsuario();
            DAOUsuario dao = new DAOUsuario();

            dto.Id = ID;

            usuario = dao.GetUsuarios(dto).First();

            ViewData["usuario"] = usuario;

            return View(usuario);
        }

        //Edicion de usuario por POST con payload
        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (usuario.ID != null)
            {
                DTOUsuario dto = new DTOUsuario();
                DAOUsuario dao = new DAOUsuario();

                dto.Id = usuario.ID;
                dto.FirstName = usuario.FirstName;
                dto.SecondName = usuario.SecondName;
                dto.UserName = usuario.Username;

                var resultado = dao.EditUsuario(dto);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var usuario = new Usuario();
            DTOUsuario dto = new DTOUsuario();
            DAOUsuario dao = new DAOUsuario();

            dto.Id = ID;

            usuario = dao.GetUsuarios(dto).First();

            ViewData["usuario"] = usuario;

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Delete(Usuario usuario)
        {
            if (usuario.ID != null)
            {
                DTOUsuario dto = new DTOUsuario();
                DAOUsuario dao = new DAOUsuario();

                dto.Id = usuario.ID;

                var resultado = dao.DeleteUsuario(dto);
            }
            return RedirectToAction("Index");
        }
    }
}
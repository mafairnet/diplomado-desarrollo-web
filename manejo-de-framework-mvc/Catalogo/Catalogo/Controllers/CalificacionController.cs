using Catalogo.Dao;
using Catalogo.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogo.Controllers
{
    public class CalificacionController : Controller
    {
        // GET: Calificacion
        public ActionResult Index()
        {
            List<string> calificaciones = new List<string>();



            DTOCalificacion dto = new DTOCalificacion();
            DAOCalificaciones dao = new DAOCalificaciones();

            DataSet ds;

            ds = dao.GetCalificion(dto);

            if (ds != null) {
                foreach (DataTable table in ds.Tables) {
                    
                    foreach (DataRow row in table.Rows){
                        var calificacionActual = row["id"].ToString() + "," + row["id_usuario"] + "," + row["calificacion"];
                        //calificaciones += calificacionActual;
                        calificaciones.Add(calificacionActual);
                    }
                    
                }
            }

            ViewBag.Title = "Listado de Calificaciones";
            ViewData["calificaciones"] = calificaciones;
            return View();
        }
    }
}

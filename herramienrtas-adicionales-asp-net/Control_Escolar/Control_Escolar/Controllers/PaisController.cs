using Control_Escolar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Control_Escolar.Controllers
{
    public class PaisController : Controller
    {
        public ActionResult Index() {
            var catalogoPaises = new List<Pais>();
            for (int i = 1; i <= 10; i++) {
                var pais = new Pais();
                pais.ID = i;
                pais.Nombre = "Pais_" + i.ToString();
                catalogoPaises.Add(pais);
            }
            ViewData["catalogoPaises"] = catalogoPaises;
            return View();
        }

        public ActionResult Add() {
            var pais = new Pais();
            ViewData["pais"] = pais;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Pais pais) {
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            var pais = new Pais();
            pais.ID = ID;
            pais.Nombre = "Pais" + ID.ToString();
            ViewData["pais"] = pais;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Pais pais)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var pais = new Pais();
            pais.ID = ID;
            pais.Nombre = "Pais" + ID.ToString();
            ViewData["pais"] = pais;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Pais pais)
        {
            return RedirectToAction("Index");
        }

    }
}
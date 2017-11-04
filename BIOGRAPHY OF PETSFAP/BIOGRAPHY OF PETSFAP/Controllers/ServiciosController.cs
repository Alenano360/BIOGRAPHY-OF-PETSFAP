using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BIOGRAPHY_OF_PETSFAP.Models;

namespace BIOGRAPHY_OF_PETSFAP.Controllers
{
    public class ServiciosController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Servicio
        public ActionResult Index()
        {
            try
            {
                ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
                var servicio = db.Servicio.Include(c => c.Estado).Where(x => x.Id_Estado == 1);
                return View(servicio.ToList());
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los Servicios.";
                return View();
            }
        }

        // GET: Servicio/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Servicio servicio = db.Servicio.Find(id);
                if (servicio == null)
                {
                    return HttpNotFound();
                }
                return View(servicio);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar el Servicio.";
                return View();
            }
        }

        // GET: Servicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Servicio servicio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    servicio.Id_Estado = 1;
                    db.Servicio.Add(servicio);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(servicio);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al crear el Servicio.";
                return View();
            }
        }

        // GET: Servicio/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Servicio servicio = db.Servicio.Find(id);
                if (servicio == null)
                {
                    return HttpNotFound();
                }
                return View(servicio);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar el Servicios.";
                return View();
            }
        }

        // POST: Servicio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Servicio servicio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    servicio.Id_Estado = 1;
                    db.Entry(servicio).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(servicio);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al editar el Servicios.";
                return View();
            }
        }

        // GET: Servicio/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Servicio servicio = db.Servicio.Find(id);
                if (servicio == null)
                {
                    return HttpNotFound();
                }
                return View(servicio);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar el Servicio.";
                return View();
            }
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Servicio servicio = db.Servicio.Find(id);
                if (servicio.Id_Estado==1)
                {
                    servicio.Id_Estado=2;
                    db.Entry(servicio).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al eliminar el Servicio.";
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

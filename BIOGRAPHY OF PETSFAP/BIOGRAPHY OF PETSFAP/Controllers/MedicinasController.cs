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
    public class MedicinasController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Medicinas
        public ActionResult Index()
        {
            var medicina = db.Medicina.Include(m => m.Estado);
            return View(medicina.ToList());
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
        }

        // GET: Medicinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicina medicina = db.Medicina.Find(id);
            if (medicina == null)
            {
                return HttpNotFound();
            }
            return View(medicina);
        }

        // GET: Medicinas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion");
            return View();
        }

        // POST: Medicinas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Medicina,Nombre,Id_Estado")] Medicina medicina)
        {
            if (ModelState.IsValid)
            {
                db.Medicina.Add(medicina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", medicina.Id_Estado);
            return View(medicina);
        }

        // GET: Medicinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicina medicina = db.Medicina.Find(id);
            if (medicina == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", medicina.Id_Estado);
            return View(medicina);
        }

        // POST: Medicinas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Medicina,Nombre,Id_Estado")] Medicina medicina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", medicina.Id_Estado);
            return View(medicina);
        }

        // GET: Medicinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicina medicina = db.Medicina.Find(id);
            if (medicina == null)
            {
                return HttpNotFound();
            }
            return View(medicina);
        }

        // POST: Medicinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicina medicina = db.Medicina.Find(id);
            db.Medicina.Remove(medicina);
            db.SaveChanges();
            return RedirectToAction("Index");
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

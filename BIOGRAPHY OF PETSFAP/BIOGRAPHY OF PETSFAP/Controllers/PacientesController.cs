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
    public class PacientesController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Pacientes
        public ActionResult Index()
        {
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            var paciente = db.Paciente.Include(p => p.Cliente).Include(p => p.Estado).Where(x => x.Id_Estado == 1);
            return View(paciente.ToList());
           
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto");
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Paciente,Id_Cliente,Animal,Nombre,Sexo,Raza,Edad,Peso,Id_Estado")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.Id_Estado = 1;
                db.Paciente.Add(paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", paciente.Id_Cliente);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", paciente.Id_Cliente);
            //ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", paciente.Id_Estado);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Paciente,Id_Cliente,Animal,Nombre,Sexo,Raza,Edad,Peso,Id_Estado")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.Id_Estado = 1;
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", paciente.Id_Cliente);
            //ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", paciente.Id_Estado);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paciente paciente = db.Paciente.Find(id);
            if(paciente.Id_Estado==1)
            {
                paciente.Id_Estado = 2;
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }
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

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
            try{
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            var paciente = db.Paciente.Include(p => p.Cliente).Include(p => p.Estado).Where(x => x.Id_Estado == 1);
            return View(paciente.ToList());
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos de los Pacientes.";
                return View();
            }
           
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            try{
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
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos del Pacientes.";
                return View();
            }
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
            try{
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
            catch (Exception)
            {
                ViewBag.Exception = "Error al crear el Pacientes.";
                return View();
            }
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            try{
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
            return View(paciente);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos del Pacientes.";
                return View();
            }
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paciente paciente)
        {
            try{
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
            catch (Exception)
            {
                ViewBag.Exception = "Error al editar el Pacientes.";
                return View();
            }
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            try{
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
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos del Pacientes.";
                return View();
            }
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try{
            Paciente paciente = db.Paciente.Find(id);
            if(paciente.Id_Estado==1)
            {
                paciente.Id_Estado = 2;
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al eliminar el Pacientes.";
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

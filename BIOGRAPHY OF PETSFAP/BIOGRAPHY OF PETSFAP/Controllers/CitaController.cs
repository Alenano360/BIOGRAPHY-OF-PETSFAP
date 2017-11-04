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
    public class CitaController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        // GET: Cita
        public ActionResult Index()
        {
            try { 
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            var cita = db.Cita.Include(c => c.Cliente).Include(c => c.Estado).Include(c => c.Detalle_Medicina).Include(c => c.Detalle_Servicio).Include(c => c.Paciente).Where(x => x.Id_Estado == 1);
            return View(cita.ToList());
            }
            catch (Exception)
            {
                ViewBag.Exception="Error al cargas las citas.";
                return View();
            }
        }

        // GET: Cita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Cita/Create
        public ActionResult Create()
        {
            ViewData["_cita.Id_Cliente"]= new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto");
            ViewData["_cita.Id_Empleado"] = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto");
            ViewData["Id_Medicina"] = new SelectList(db.Producto.Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");
            ViewData["_cita.Id_Paciente"] = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto");
            ViewData["Id_Servicio"] = new SelectList(db.Servicio.Where(x => x.Id_Estado == 1), "Id_Servicio", "Nombre");
            return View();
        }

        // POST: Cita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cita cita)
        {
            if (ModelState.IsValid)
            {
                cita.Id_Estado = 1;
                db.Cita.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["_cita.Id_Cliente"] = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", cita.Id_Cliente);
            ViewData["_cita.Id_Empleado"] = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", cita.Id_Empleado);
            ViewData["Id_Medicina"] = new SelectList(db.Producto.Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");
            ViewData["_cita.Id_Paciente"] = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto", cita.Id_Paciente);
            ViewData["Id_Servicio"] = new SelectList(db.Servicio.Where(x => x.Id_Estado == 1), "Id_Servicio", "Nombre");
            return View(cita);
        }

        // GET: Cita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewData["_cita.Id_Cliente"] = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", cita.Id_Cliente);
            ViewData["_cita.Id_Empleado"] = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", cita.Id_Empleado);
            ViewData["Id_Medicina"] = new SelectList(db.Producto.Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");
            ViewData["_cita.Id_Paciente"] = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto", cita.Id_Paciente);
            ViewData["Id_Servicio"] = new SelectList(db.Servicio.Where(x => x.Id_Estado == 1), "Id_Servicio", "Nombre");
            return View(cita);
        }

        // POST: Cita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cita cita)
        {
            if (ModelState.IsValid)
            {
                cita.Id_Estado = 1;
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["_cita.Id_Cliente"] = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", cita.Id_Cliente);
            ViewData["_cita.Id_Empleado"] = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", cita.Id_Empleado);
            ViewData["Id_Medicina"] = new SelectList(db.Producto.Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");
            ViewData["_cita.Id_Paciente"] = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto", cita.Id_Paciente);
            ViewData["Id_Servicio"] = new SelectList(db.Servicio.Where(x => x.Id_Estado == 1), "Id_Servicio", "Nombre");

            return View(cita);
        }

        // GET: Cita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Cita.Find(id);
            if(cita.Id_Estado==1)
            {
                cita.Id_Estado = 2;
                db.Entry(cita).State = EntityState.Modified;
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
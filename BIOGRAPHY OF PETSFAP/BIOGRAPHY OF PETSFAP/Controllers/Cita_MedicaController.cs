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
    public class Cita_MedicaController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Cita_Medica
        public ActionResult Index()
        {
            var cita_Medica = db.Cita_Medica.Include(c => c.Cliente).Include(c => c.Estado).Include(c => c.Medicina).Include(c => c.Paciente);
            return View(cita_Medica.ToList());
        }

        // GET: Cita_Medica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita_Medica cita_Medica = db.Cita_Medica.Find(id);
            if (cita_Medica == null)
            {
                return HttpNotFound();
            }
            return View(cita_Medica);
        }

        // GET: Cita_Medica/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Id_Cliente");
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion");
            ViewBag.Id_Medicina = new SelectList(db.Medicina, "Id_Medicina", "Nombre");
            ViewBag.Id_Paciente = new SelectList(db.Paciente, "Id_Paciente", "Animal");
            return View();
        }

        // POST: Cita_Medica/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cita_Medica,Fecha,Hora,Id_Medicina,Id_Cliente,Id_Paciente,Descripcion,Id_Estado")] Cita_Medica cita_Medica)
        {
            if (ModelState.IsValid)
            {
                db.Cita_Medica.Add(cita_Medica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Id_Cliente", cita_Medica.Id_Cliente);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", cita_Medica.Id_Estado);
            ViewBag.Id_Medicina = new SelectList(db.Medicina, "Id_Medicina", "Nombre", cita_Medica.Id_Medicina);
            ViewBag.Id_Paciente = new SelectList(db.Paciente, "Id_Paciente", "Animal", cita_Medica.Id_Paciente);
            return View(cita_Medica);
        }

        // GET: Cita_Medica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita_Medica cita_Medica = db.Cita_Medica.Find(id);
            if (cita_Medica == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Id_Cliente", cita_Medica.Id_Cliente);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", cita_Medica.Id_Estado);
            ViewBag.Id_Medicina = new SelectList(db.Medicina, "Id_Medicina", "Nombre", cita_Medica.Id_Medicina);
            ViewBag.Id_Paciente = new SelectList(db.Paciente, "Id_Paciente", "Animal", cita_Medica.Id_Paciente);
            return View(cita_Medica);
        }

        // POST: Cita_Medica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cita_Medica,Fecha,Hora,Id_Medicina,Id_Cliente,Id_Paciente,Descripcion,Id_Estado")] Cita_Medica cita_Medica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita_Medica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Id_Cliente", cita_Medica.Id_Cliente);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", cita_Medica.Id_Estado);
            ViewBag.Id_Medicina = new SelectList(db.Medicina, "Id_Medicina", "Nombre", cita_Medica.Id_Medicina);
            ViewBag.Id_Paciente = new SelectList(db.Paciente, "Id_Paciente", "Animal", cita_Medica.Id_Paciente);
            return View(cita_Medica);
        }

        // GET: Cita_Medica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita_Medica cita_Medica = db.Cita_Medica.Find(id);
            if (cita_Medica == null)
            {
                return HttpNotFound();
            }
            return View(cita_Medica);
        }

        // POST: Cita_Medica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita_Medica cita_Medica = db.Cita_Medica.Find(id);
            db.Cita_Medica.Remove(cita_Medica);
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

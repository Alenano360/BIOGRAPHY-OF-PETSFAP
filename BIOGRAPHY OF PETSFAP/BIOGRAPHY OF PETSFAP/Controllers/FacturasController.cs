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
    public class FacturasController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Facturas
        public ActionResult Index()
        {
            var factura = db.Factura.Include(f => f.Cliente).Include(f => f.Empleado).Include(f => f.Estado1).Include(f => f.Proveedor);
            return View(factura.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Id_Cliente");
            ViewBag.Id_Empleado = new SelectList(db.Empleado, "Id_Empleado", "Id_Empleado");
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion");
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor, "Id_Proveedor", "Nombre_Empresa");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Numero_Factura,Id_Empleado,Id_Cliente,Id_Proveedor,Estado,Fecha,Precio_Total,Id_Estado")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Id_Cliente", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado, "Id_Empleado", "Id_Empleado", factura.Id_Empleado);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", factura.Id_Estado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor, "Id_Proveedor", "Nombre_Empresa", factura.Id_Proveedor);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Id_Cliente", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado, "Id_Empleado", "Id_Empleado", factura.Id_Empleado);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", factura.Id_Estado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor, "Id_Proveedor", "Nombre_Empresa", factura.Id_Proveedor);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Numero_Factura,Id_Empleado,Id_Cliente,Id_Proveedor,Estado,Fecha,Precio_Total,Id_Estado")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Id_Cliente", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado, "Id_Empleado", "Id_Empleado", factura.Id_Empleado);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", factura.Id_Estado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor, "Id_Proveedor", "Nombre_Empresa", factura.Id_Proveedor);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
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

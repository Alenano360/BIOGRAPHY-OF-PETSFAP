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
            var factura = db.Factura.Include(f => f.Cliente).Include(f => f.Empleado).Include(f => f.Estado).Include(f => f.Proveedor).Include(f=>f.Detalle_Factura).Where(x=>x.Id_Estado==1);
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            return View(factura.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.FirstOrDefault(x => x.Numero_Factura == id);
            Detalle_Factura detalle = db.Detalle_Factura.FirstOrDefault(x => x.Numero_Factura == id);
            //Producto producto = db.Producto.FirstOrDefault(x => x.Id_Producto==x.);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto");
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto");
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto");
            ViewBag.Id_Producto = new SelectList(db.Producto.Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");

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

            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", factura.Id_Empleado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", factura.Id_Proveedor);
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
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", factura.Id_Empleado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", factura.Id_Proveedor);
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
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", factura.Id_Empleado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", factura.Id_Proveedor);
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

        [System.Web.Services.WebMethod]
        public string SelectPrecio(int id)
        {
            var precio=db.Producto.Where(x => x.Id_Producto == id).Select(x=>x.Precio).FirstOrDefault();
            return Convert.ToString( precio);

        }
    }
}

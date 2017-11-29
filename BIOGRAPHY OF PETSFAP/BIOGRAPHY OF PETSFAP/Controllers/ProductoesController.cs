using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BIOGRAPHY_OF_PETSFAP.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace BIOGRAPHY_OF_PETSFAP.Controllers
{
    public class ProductoesController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Productoes
        public ActionResult Index()
        {
            try{
            var producto = db.Producto.Include(p => p.Estado).Include(p => p.Proveedor).Include(p => p.Categoria).Where(x => x.Id_Estado == 1);
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            return View(producto.ToList());
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los Productos.";
                return View();
            }
           
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los Productos.";
                return View();
            }
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto");
            ViewBag.Id_Categoria = new SelectList(db.Categoria, "Id_Categoria", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            try{
            if (ModelState.IsValid)
            {
                producto.Id_Estado = 1;
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", producto.Id_Proveedor);
            ViewBag.Id_Categoria = new SelectList(db.Categoria, "Id_Categoria", "Nombre", producto.Id_Categoria);
            return View(producto);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al crear los Productos.";
                return View();
            }

        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Categoria = new SelectList(db.Categoria, "Id_Categoria", "Nombre", producto.Id_Categoria);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", producto.Id_Proveedor);
            return View(producto);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos del Porducto.";
                return View();
            }
        }

        // POST: Productoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producto producto)
        {
            try{
            if (ModelState.IsValid)
            {
                producto.Id_Estado = 1;
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //else
            //{
            //    ViewBag.Exception = "Error. El modelo de Producto no es valido.";
            //    return View();
            //}
            ViewBag.Id_Categoria = new SelectList(db.Categoria, "Id_Categoria", "Nombre", producto.Id_Categoria);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", producto.Id_Proveedor);
            return View(producto);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al Editar el Porducto.";
                return View();
            }
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos del Porducto.";
                return View();
            }
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try{
            Producto productos = db.Producto.Find(id);
            if (productos.Id_Estado == 1)
            {
                productos.Id_Estado = 2;
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al Eliminar el Porducto.";
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

        public ActionResult getReport()
        {
            List<Producto> productos = db.Producto.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "Productos.rpt"));
            rd.SetDataSource(productos);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                Response.AddHeader("Content-Disposition", "inline; filename=Productos.pdf");
                return File(stream, "application/pdf");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

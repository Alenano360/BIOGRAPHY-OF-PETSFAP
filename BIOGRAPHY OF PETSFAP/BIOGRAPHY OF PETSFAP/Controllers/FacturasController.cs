using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BIOGRAPHY_OF_PETSFAP.Models;
using Newtonsoft.Json;
using BIOGRAPHY_OF_PETSFAP.Class;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace BIOGRAPHY_OF_PETSFAP.Controllers
{
    public class FacturasController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        public static List<Detalle_Factura> listDetalles = new List<Detalle_Factura>();
        public static List<Detalle_Factura> listaDetalles = new List<Detalle_Factura>();

        // GET: Facturas
        public ActionResult Index()
        {
            try { 
            var factura = db.Factura.Include(f => f.Cliente).Include(f => f.Empleado).Include(f => f.Estado).Include(f => f.Proveedor).Include(f => f.Detalle_Factura).Where(x => x.Id_Estado == 1);
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            return View(factura.ToList());
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargas las Facturas.";
                return View();
            }
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.FirstOrDefault(x => x.Numero_Factura == id);
            var detalle = db.Detalle_Factura.Include(f => f.Producto).Where(m => m.Numero_Factura == id);
            listaDetalles = null;
            listaDetalles = detalle.ToList();
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", factura.Id_Empleado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", factura.Id_Proveedor);
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewData["_factura.Id_Cliente"] = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto");
            ViewData["_factura.Id_Empleado"] = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto");
            ViewData["_factura.Id_Proveedor"] = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto");
            ViewBag.Id_Producto = new SelectList(db.Producto.Include(x => x.Categoria).Where(x => x.Categoria.Nombre.Equals("Producto")).Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");
            return View();
        }

        // POST: Facturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Facturacion_Poco factura_poco)
        {
            if (ModelState.IsValid)
            {
                Factura factura = new Factura
                {
                    Id_Empleado = factura_poco._factura.Id_Empleado,
                    Id_Cliente = factura_poco._factura.Id_Cliente,
                    Fecha = factura_poco._factura.Fecha,
                    Id_Estado = 1,
                    Precio_Total = factura_poco._factura.Precio_Total,
                    Id_Proveedor = null
                };

                db.Factura.Add(factura);
                db.SaveChanges();
                foreach (Detalle_Factura detalle in listDetalles)
                {
                    detalle.Numero_Factura = factura.Numero_Factura;
                    db.Detalle_Factura.Add(detalle);
                    var producto = db.Producto.FirstOrDefault(x => x.Id_Producto == detalle.Id_Producto);
                    producto.Cantidad -= detalle.Cantidad;
                    db.Entry(producto).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(factura_poco);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.FirstOrDefault(x => x.Numero_Factura == id);
            var detalle = db.Detalle_Factura.Where(m => m.Numero_Factura == id);
            listaDetalles = null;
            listaDetalles = detalle.ToList();
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", factura.Id_Empleado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", factura.Id_Proveedor);
            ViewBag.Id_Producto = new SelectList(db.Producto.Where(x => x.Id_Estado == 1).Where(x => x.Categoria.Nombre.Equals("Producto")), "Id_Producto", "Nombre");
            return View(factura);
        }


        // POST: Facturas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Factura factura)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var facturaEdit = db.Factura.FirstOrDefault(x => x.Numero_Factura == factura.Numero_Factura);
                    facturaEdit.Id_Empleado = factura.Id_Empleado;
                    facturaEdit.Id_Cliente = factura.Id_Cliente;
                    facturaEdit.Fecha = factura.Fecha;
                    facturaEdit.Id_Estado = 1;
                    facturaEdit.Precio_Total = factura.Precio_Total;
                    facturaEdit.Id_Proveedor = null;
                    db.Entry(facturaEdit).State = EntityState.Modified;
                    List<int> listDetallesBaseDatos = listaDetalles.Select(x => x.Id_Detalle).ToList();
                    List<int> listDetallesDataTable = listDetalles.Select(x => x.Id_Detalle).ToList();
                    var nuevos = listDetallesDataTable.Where(x => !listDetallesBaseDatos.Contains(x));
                    var eliminados = listDetallesBaseDatos.Where(x => !listDetallesDataTable.Contains(x));
                    var editados = listDetallesDataTable.Where(x => listDetallesBaseDatos.Contains(x));
                    foreach (Detalle_Factura detalle in listDetalles)
                    {
                        if (nuevos.Contains(detalle.Id_Detalle))
                        {
                            Detalle_Factura newDetalle = new Detalle_Factura
                            {
                                Id_Detalle = detalle.Id_Detalle,
                                Cantidad = detalle.Cantidad,
                                Id_Producto = detalle.Id_Producto,
                                Numero_Factura = detalle.Numero_Factura,
                                Precio_Total_Producto = detalle.Precio_Total_Producto,
                                Precio_Unitario = detalle.Precio_Unitario
                            };
                            db.Detalle_Factura.Add(newDetalle);
                        }
                        else if (editados.Contains(detalle.Id_Detalle))
                        {
                            var edit = db.Detalle_Factura.FirstOrDefault(x => x.Id_Detalle == detalle.Id_Detalle);
                            edit.Id_Detalle = detalle.Id_Detalle;
                            edit.Id_Producto = detalle.Id_Producto;
                            edit.Numero_Factura = detalle.Numero_Factura;
                            edit.Precio_Total_Producto = detalle.Precio_Total_Producto;
                            edit.Precio_Unitario = detalle.Precio_Unitario;
                            edit.Cantidad = detalle.Cantidad;
                            db.Entry(edit).State = EntityState.Modified;
                        }

                    }
                    foreach (Detalle_Factura detalle in listaDetalles)
                    {
                        if (eliminados.Contains(detalle.Id_Detalle))
                        {
                            var edit = db.Detalle_Factura.FirstOrDefault(x => x.Id_Detalle == detalle.Id_Detalle);
                            db.Detalle_Factura.Remove(edit);
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + Server.HtmlEncode(ex.ToString()) + "')</script>");
            }
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.FirstOrDefault(x => x.Numero_Factura == id);
            var detalle = db.Detalle_Factura.Include(f => f.Producto).Where(m => m.Numero_Factura == id);
            listaDetalles = null;
            listaDetalles = detalle.ToList();
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", factura.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", factura.Id_Empleado);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedor.Where(x => x.Id_Estado == 1), "Id_Proveedor", "NombreCompleto", factura.Id_Proveedor);
            return View(factura);
        }
        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.FirstOrDefault(x => x.Numero_Factura == id);
            if (factura.Id_Estado == 1)
            {
                factura.Id_Estado = 2;
                db.Entry(factura).State = EntityState.Modified;
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
        [System.Web.Services.WebMethod]
        public string SelectPrecio(int id)
        {
            var precio = db.Producto.Where(x => x.Id_Producto == id).Select(x => x.Precio).FirstOrDefault();
            return Convert.ToString(precio);

        }
        [System.Web.Services.WebMethod]
        public string Detalle(string rows)
        {
            try
            {
                var detalles = JsonConvert.DeserializeObject<List<Detalle_Factura>>(rows);
                listDetalles = detalles;
                return "ok";
            }
            catch (Exception)
            {
                return "error";
            }
        }
        [System.Web.Services.WebMethod]
        public string setJson()
        {
            List<Detalle_Factura> rows = listaDetalles;
            string json = "[";

            for (int i = 0, len = rows.Count; i < len; i++)
            {
                if (i == 0)
                {
                    json += "['" + rows[i].Id_Producto + "'" +
                    ",'" + rows[i].Producto.Nombre + "'" +
                    ",'" + rows[i].Cantidad + "'" +
                    ",'" + rows[i].Precio_Unitario + "'" +
                    ",'" + rows[i].Precio_Total_Producto + "'" +
                    ",'" + rows[i].Id_Detalle + "']";
                }
                else
                {
                    json += ",['" + rows[i].Id_Producto + "'" +
                    ",'" + rows[i].Producto.Nombre + "'" +
                    ",'" + rows[i].Cantidad + "'" +
                    ",'" + rows[i].Precio_Unitario + "'" +
                    ",'" + rows[i].Precio_Total_Producto + "'" +
                    ",'" + rows[i].Id_Detalle + "']";
                }

            }
            json += ']';
            return json;
        }

        public ActionResult getReport()
        {
            List<Factura> factura = new List<Factura>();
            factura = db.Factura.Include(x => x.Detalle_Factura).Include(x => x.Cliente).Include(x => x.Empleado).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "Facturas.rpt"));
            rd.SetDataSource(factura);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                Response.AddHeader("Content-Disposition", "inline; filename=Factura.pdf");
                return File(stream, "application/pdf");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult getReportFact()
        {
            List<Factura> factura = new List<Factura>();
            factura = db.Factura.Include(x => x.Detalle_Factura).Include(x => x.Cliente).Include(x => x.Empleado).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "TotalFacturas.rpt"));
            rd.SetDataSource(factura);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                Response.AddHeader("Content-Disposition", "inline; filename=TotalFactura.pdf");
                return File(stream, "application/pdf");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

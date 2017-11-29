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
    public class CitaController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        public static List<Detalle_Medicina> listMedicina = new List<Detalle_Medicina>();
        public static List<Detalle_Medicina> listaMedicina = new List<Detalle_Medicina>();
        public static List<Detalle_Servicio> listServicio = new List<Detalle_Servicio>();
        public static List<Detalle_Servicio> listaServicio = new List<Detalle_Servicio>();
        // GET: Cita
        public ActionResult Index()
        {
            try
            {
                ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
                var cita = db.Cita.Include(c => c.Cliente).Include(c => c.Estado).Include(c => c.Detalle_Medicina).Include(c => c.Detalle_Servicio).Include(c => c.Paciente).Where(x => x.Id_Estado == 1);
                return View(cita.ToList());
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargas las citas.";
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
            Cita cita = db.Cita.FirstOrDefault(x => x.Id_Cita == id);
            var detalleMedicina = db.Detalle_Medicina.Include(f => f.Producto).Where(m => m.Id_Cita == id);
            listaMedicina = null;
            listaMedicina = detalleMedicina.ToList();
            var detalleServicio = db.Detalle_Servicio.Include(f => f.Servicio).Where(m => m.Id_Cita == id);
            listaServicio = null;
            listaServicio = detalleServicio.ToList();
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", cita.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", cita.Id_Empleado);
            ViewBag.Id_Paciente = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto", cita.Id_Paciente);
            return View(cita);
        }

        // GET: Cita/Create
        public ActionResult Create()
        {
            ViewData["_cita.Id_Cliente"] = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto");
            ViewData["_cita.Id_Empleado"] = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto");
            ViewData["_detalle_medicina.Producto"] = new SelectList(db.Producto.Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");
            ViewData["_cita.Id_Paciente"] = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto");
            ViewData["_detalle_servicio.Id_Servicio"] = new SelectList(db.Servicio.Where(x => x.Id_Estado == 1), "Id_Servicio", "Nombre");
            return View();
        }

        // POST: Cita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cita_Medica_Poco cita_Medicina_Poco)
        {
            if (ModelState.IsValid)
            {
                Cita cita = new Cita
                {
                    Costo_Total = cita_Medicina_Poco._cita.Costo_Total,
                    Descripcion = cita_Medicina_Poco._cita.Descripcion,
                    Estado_Cita = cita_Medicina_Poco._cita.Estado_Cita,
                    Fecha = cita_Medicina_Poco._cita.Fecha,
                    Hora_Final = cita_Medicina_Poco._cita.Hora_Final,
                    Hora_Inico = cita_Medicina_Poco._cita.Hora_Inico,
                    Id_Cliente = cita_Medicina_Poco._cita.Id_Cliente,
                    Id_Empleado = cita_Medicina_Poco._cita.Id_Empleado,
                    Id_Paciente = cita_Medicina_Poco._cita.Id_Paciente,
                    Id_Estado = 1
                };
                db.Cita.Add(cita);
                db.SaveChanges();
                foreach (Detalle_Medicina detalle in listMedicina)
                {
                    detalle.Id_Cita = cita.Id_Cita;
                    db.Detalle_Medicina.Add(detalle);
                    var producto = db.Producto.FirstOrDefault(x => x.Id_Producto == detalle.Id_Producto);
                    producto.Cantidad -= detalle.Cantidad;
                    db.Entry(producto).State = EntityState.Modified;
                }
                db.SaveChanges();
                foreach (Detalle_Servicio detalle in listServicio)
                {
                    detalle.Id_Cita = cita.Id_Cita;
                    db.Detalle_Servicio.Add(detalle);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["_cita.Id_Cliente"] = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", cita_Medicina_Poco._cita.Id_Cliente);
            ViewData["_cita.Id_Empleado"] = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", cita_Medicina_Poco._cita.Id_Empleado);
            ViewData["_detalle_medicina.Producto"] = new SelectList(db.Producto.Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");
            ViewData["_cita.Id_Paciente"] = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto", cita_Medicina_Poco._cita.Id_Paciente);
            ViewData["_detalle_servicio.Id_Servicio"] = new SelectList(db.Servicio.Where(x => x.Id_Estado == 1), "Id_Servicio", "Nombre");
            return View(cita_Medicina_Poco);
        }

        // GET: Cita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.FirstOrDefault(x => x.Id_Cita == id);
            var detalleMedicina = db.Detalle_Medicina.Include(f => f.Producto).Where(m => m.Id_Cita == id);
            listaMedicina = null;
            listaMedicina = detalleMedicina.ToList();
            var detalleServicio = db.Detalle_Servicio.Include(f => f.Servicio).Where(m => m.Id_Cita == id);
            listaServicio = null;
            listaServicio = detalleServicio.ToList();
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", cita.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", cita.Id_Empleado);
            ViewBag.Id_Paciente = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto", cita.Id_Paciente);
            ViewBag.Id_Servicio = new SelectList(db.Servicio.Where(x => x.Id_Estado == 1), "Id_Servicio", "Nombre");
            ViewBag.Id_Medicina = new SelectList(db.Producto.Where(x => x.Id_Estado == 1), "Id_Producto", "Nombre");

            return View(cita);
        }

        // POST: Cita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cita cita)
        {
            try {
                if (ModelState.IsValid)
                {
                    var citaEdit = db.Cita.FirstOrDefault(x => x.Id_Cita == cita.Id_Cita);
                    citaEdit.Id_Empleado = cita.Id_Empleado;
                    citaEdit.Costo_Total = cita.Costo_Total;
                    citaEdit.Descripcion = cita.Descripcion;
                    citaEdit.Estado_Cita = citaEdit.Estado_Cita;
                    citaEdit.Fecha = cita.Fecha;
                    citaEdit.Hora_Final = cita.Hora_Final;
                    citaEdit.Hora_Inico = cita.Hora_Inico;
                    citaEdit.Id_Cliente = cita.Id_Cliente;
                    citaEdit.Id_Paciente = cita.Id_Paciente;
                    citaEdit.Id_Estado = 1;
                    db.Entry(citaEdit).State = EntityState.Modified;
                    List<int> listaMedicinaBaseDatos = listaMedicina.Select(x => x.Id_Detalle_Medicina).ToList();
                    List<int> listaMedicinaDataTable = listMedicina.Select(x => x.Id_Detalle_Medicina).ToList();
                    List<int> listaServicioBaseDatos = listaServicio.Select(x => x.Id_Detalle_Servicio).ToList();
                    List<int> listaServicioDataTable = listServicio.Select(x => x.Id_Detalle_Servicio).ToList();
                    var nuevosMedicina = listaMedicinaDataTable.Where(x => !listaMedicinaBaseDatos.Contains(x));
                    var eliminadosMedicina = listaMedicinaBaseDatos.Where(x => !listaMedicinaDataTable.Contains(x));
                    var editadosMedicina = listaMedicinaDataTable.Where(x => listaMedicinaBaseDatos.Contains(x));
                    var nuevosServicio = listaServicioDataTable.Where(x => !listaServicioBaseDatos.Contains(x));
                    var eliminadosServicio = listaServicioBaseDatos.Where(x => !listaServicioDataTable.Contains(x));
                    var editadosServicio = listaServicioDataTable.Where(x => listaServicioBaseDatos.Contains(x));
                    foreach (Detalle_Medicina detalleMedicina in listMedicina)
	                {
		                if (nuevosMedicina.Contains(detalleMedicina.Id_Detalle_Medicina))
	                    {
		                    Detalle_Medicina newdetalleMedicina = new Detalle_Medicina{
                                Id_Detalle_Medicina=detalleMedicina.Id_Detalle_Medicina,
                                Cantidad=detalleMedicina.Cantidad,
                                Id_Producto=detalleMedicina.Id_Producto,
                                Precio_Total=detalleMedicina.Precio_Total
                            };
                            db.Detalle_Medicina.Add(newdetalleMedicina);
	                    }else if(editadosMedicina.Contains(detalleMedicina.Id_Detalle_Medicina)){
                            var editMedicina = db.Detalle_Medicina.FirstOrDefault(x=>x.Id_Detalle_Medicina==detalleMedicina.Id_Detalle_Medicina);
                            editMedicina.Cantidad=detalleMedicina.Cantidad;
                            editMedicina.Id_Cita=detalleMedicina.Id_Cita;
                            editMedicina.Id_Detalle_Medicina=detalleMedicina.Id_Detalle_Medicina;
                            editMedicina.Id_Producto=detalleMedicina.Id_Producto;
                            editMedicina.Precio_Total=detalleMedicina.Precio_Total;
                            db.Entry(editMedicina).State=EntityState.Modified;
                        }
	                }
                    foreach(Detalle_Medicina detalleMedicina in listaMedicina){
                        if(eliminadosMedicina.Contains(detalleMedicina.Id_Detalle_Medicina)){
                            var deleteMedicina = db.Detalle_Medicina.FirstOrDefault(x=>x.Id_Detalle_Medicina==detalleMedicina.Id_Detalle_Medicina);
                            db.Detalle_Medicina.Remove(detalleMedicina);
                        }
                    }

                    foreach (Detalle_Servicio detalleServicio in listServicio)
                    {
                        if (nuevosServicio.Contains(detalleServicio.Id_Detalle_Servicio))
                        {
                            Detalle_Servicio newdetalleServicio = new Detalle_Servicio
                            {
                                Id_Detalle_Servicio = detalleServicio.Id_Detalle_Servicio,
                                Cantidad = detalleServicio.Cantidad,
                                Id_Servicio = detalleServicio.Id_Servicio,
                                Precio_Total = detalleServicio.Precio_Total
                            };
                            db.Detalle_Servicio.Add(newdetalleServicio);
                        }
                        else if (editadosServicio.Contains(detalleServicio.Id_Detalle_Servicio))
                        {
                            var editServicio = db.Detalle_Servicio.FirstOrDefault(x => x.Id_Detalle_Servicio == detalleServicio.Id_Detalle_Servicio);
                            editServicio.Cantidad = detalleServicio.Cantidad;
                            editServicio.Id_Cita = detalleServicio.Id_Cita;
                            editServicio.Id_Detalle_Servicio = detalleServicio.Id_Detalle_Servicio;
                            editServicio.Id_Servicio = detalleServicio.Id_Servicio;
                            editServicio.Precio_Total = detalleServicio.Precio_Total;
                            db.Entry(editServicio).State = EntityState.Modified;
                        }
                    }
                    foreach (Detalle_Servicio detalleServicio in listaServicio)
                    {
                        if (eliminadosServicio.Contains(detalleServicio.Id_Detalle_Servicio))
                        {
                            var deleteServicio = db.Detalle_Servicio.FirstOrDefault(x => x.Id_Detalle_Servicio == detalleServicio.Id_Detalle_Servicio);
                            db.Detalle_Servicio.Remove(detalleServicio);
                        }
                    }
                     db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
             Response.Write("<script>alert('" + Server.HtmlEncode(ex.ToString()) + "')</script>");
            }
            return View(cita);
        }

        // GET: Cita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.FirstOrDefault(x => x.Id_Cita == id);
            var detalleMedicina = db.Detalle_Medicina.Include(f => f.Producto).Where(m => m.Id_Cita == id);
            listaMedicina = null;
            listaMedicina = detalleMedicina.ToList();
            var detalleServicio = db.Detalle_Servicio.Include(f => f.Servicio).Where(m => m.Id_Cita == id);
            listaServicio = null;
            listaServicio = detalleServicio.ToList();
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente.Where(x => x.Id_Estado == 1), "Id_Cliente", "NombreCompleto", cita.Id_Cliente);
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", cita.Id_Empleado);
            ViewBag.Id_Paciente = new SelectList(db.Paciente.Where(x => x.Id_Estado == 1), "Id_Paciente", "PacienteCompleto", cita.Id_Paciente);

            return View(cita);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Cita.FirstOrDefault(x => x.Id_Cita == id);
            if (cita.Id_Estado == 1)
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

        [System.Web.Services.WebMethod]
        public string SelectPrecioServicio(int id)
        {
            var precio = db.Servicio.Where(x => x.Id_Servicio == id).Select(x => x.Costo).FirstOrDefault();
            return Convert.ToString(precio);

        }
        [System.Web.Services.WebMethod]
        public string DetalleServicio(string rows)
        {
            try
            {
                var detalles = JsonConvert.DeserializeObject<List<Detalle_Servicio>>(rows);
                listServicio = detalles;
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
            List<Detalle_Servicio> rows = listaServicio;
            string json = "[";

            for (int i = 0, len = rows.Count; i < len; i++)
            {
                if (i == 0)
                {
                    json += "['" + rows[i].Id_Servicio + "'" +
                    ",'" + rows[i].Servicio.Nombre + "'" +
                    ",'" + rows[i].Cantidad + "'" +
                    ",'" + rows[i].Servicio.Costo + "'" +
                    ",'" + rows[i].Precio_Total + "'" +
                    ",'" + rows[i].Id_Detalle_Servicio + "']";
                }
                else
                {
                    json += ",['" + rows[i].Id_Servicio + "'" +
                    ",'" + rows[i].Servicio.Nombre + "'" +
                    ",'" + rows[i].Cantidad + "'" +
                    ",'" + rows[i].Servicio.Costo + "'" +
                    ",'" + rows[i].Precio_Total + "'" +
                    ",'" + rows[i].Id_Detalle_Servicio + "']";
                }
            }
            json += ']';
            return json;
        }
    }
}
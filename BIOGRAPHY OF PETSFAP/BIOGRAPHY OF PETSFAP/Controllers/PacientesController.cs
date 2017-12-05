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
using Microsoft.Reporting.WebForms;

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

        //public ActionResult Report(string id, int idP, int idC)
        //{
        //    LocalReport lr = new LocalReport();
        //    string path = Path.Combine(Server.MapPath("~/Reportes"), "Expedientes.rdlc");
        //    if (System.IO.File.Exists(path))
        //    {
        //        lr.ReportPath = path;
        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
        //    var data = (from p in db.Cita.Where(x => x.Id_Paciente == idP)
        //                select new
        //                {
        //                    Costo_Total = p.Costo_Total,
        //                    Fecha = p.Fecha,
        //                    Descripcion = p.Descripcion,
        //                    Precio_Total = p.Estado_Cita,
        //                    Hora_Final = p.Hora_Final,
        //                    Hora_Inico = p.Hora_Inico,
        //                    Id_Cita = p.Id_Cita,
        //                    Id_Cliente = p.Cliente.Persona.Nombre + " " + p.Cliente.Persona.Apellidos,
        //                    Id_Empleado = p.Empleado.Persona.Nombre + " " + p.Empleado.Persona.Apellidos,
        //                    Id_Paciente = p.Paciente.Animal + " " + p.Paciente.Raza + " " + p.Paciente.Nombre,
        //                }).ToList();

        //    var detalleM = (from p in db.Detalle_Medicina.Where(x => x.Id_Cita == data.Id_Cita)
        //                    select new
        //                    {
        //                        Cantidad = p.Cantidad,
        //                        Id_Producto = p.Producto.Nombre,
        //                        Precio_Total = p.Precio_Total,
        //                    }).ToList();
        //    var detalleS = (from p in db.Detalle_Servicio.Where(x => x.Id_Cita == Id_Cita)
        //                    select new
        //                    {
        //                        Cantidad = p.Cantidad,
        //                        Id_Servicio = p.Servicio.Nombre,
        //                        Precio_Total = p.Precio_Total,
        //                    }).ToList();
        //    ReportDataSource rd = new ReportDataSource("dsFactura", data);
        //    lr.DataSources.Add(rd);
        //    ReportDataSource rd1 = new ReportDataSource("dsDetalleMedicina", detalleM);
        //    lr.DataSources.Add(rd1);
        //    ReportDataSource rd2 = new ReportDataSource("dsDetalleServicio", detalleS);
        //    lr.DataSources.Add(rd2);
        //    string reportType = id;
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;
        //    string deviceInfo =
        //    "<DeviceInfo>" +
        //    "  <OutputFormat>" + id + "</OutputFormat>" +
        //    "  <PageWidth>8.5in</PageWidth>" +
        //    "  <PageHeight>11in</PageHeight>" +
        //    "  <MarginTop>0.5in</MarginTop>" +
        //    "  <MarginLeft>1in</MarginLeft>" +
        //    "  <MarginRight>1in</MarginRight>" +
        //    "  <MarginBottom>0.5in</MarginBottom>" +
        //    "</DeviceInfo>";
        //    Warning[] warnings;
        //    string[] streams;
        //    byte[] renderedBytes;
        //    renderedBytes = lr.Render(
        //        reportType,
        //        deviceInfo,
        //        out mimeType,
        //        out encoding,
        //        out fileNameExtension,
        //        out streams,
        //        out warnings);
        //    return File(renderedBytes, mimeType);
        //}
    }
}

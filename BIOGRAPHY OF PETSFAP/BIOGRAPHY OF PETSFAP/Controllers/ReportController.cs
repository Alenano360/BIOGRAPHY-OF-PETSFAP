using BIOGRAPHY_OF_PETSFAP.Models;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIOGRAPHY_OF_PETSFAP.Controllers
{
    public class ReportController : Controller
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        // GET: Report
        public ActionResult Index()
        {           
            return View();
        }
        public ActionResult getReport()
        {
            List<Medicina> medicamentos = new List<Medicina>();
            medicamentos = db.Medicina.ToList();            
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "medicinas.rpt"));
            rd.SetDataSource(medicamentos);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                Response.AddHeader("Content-Disposition", "inline; filename=Medicinas.pdf");
                return File(stream,"application/pdf");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
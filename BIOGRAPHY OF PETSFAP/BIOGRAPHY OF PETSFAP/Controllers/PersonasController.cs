using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BIOGRAPHY_OF_PETSFAP.Models;
using BIOGRAPHY_OF_PETSFAP.Class;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace BIOGRAPHY_OF_PETSFAP.Controllers
{

    public class PersonasController : Controller
    {
        private IEnumerable<Persona> ListaPersona;
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Personas
        public ActionResult Index()
        {
            try{
            ListaPersona = db.Persona.Include(c => c.Cliente).Include(c => c.Estado).Include(c => c.Empleado).Include(c => c.Proveedor).Where(x => x.Id_Estado == 1);
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            return View(ListaPersona);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar las Personas.";
                return View();
            }
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona per = db.Persona.FirstOrDefault(x => x.Id_Persona == id);
            Cliente cli = db.Cliente.FirstOrDefault(x => x.Id_Persona == id);
            Empleado emp = db.Empleado.FirstOrDefault(x => x.Id_Persona == id);
            Proveedor pro = db.Proveedor.FirstOrDefault(x => x.Id_Persona == id);
            Persona_Poco persona = new Persona_Poco()
            {
                Id_Persona = per.Id_Persona,
                Nombre = per.Nombre,
                Apellidos = per.Apellidos,
                Direccion = per.Direccion,
                Telefono = per.Telefono,
                Id_Estado = per.Id_Estado,
                Chk_Cliente = per.Chk_Cliente,
                Chk_Empleado = per.Chk_Empleado,
                Chk_Proveedor = per.Chk_Proveedor
            };
            Cliente_Poco cliente = null;
            if (cli != null)
            {
                cliente = new Cliente_Poco()
                {
                    Id_Cliente = cli.Id_Cliente,
                    Id_Estado = cli.Id_Estado,
                    Id_Persona = cli.Id_Persona,
                };
            }
            Empleado_Poco empleado = null;
            if (emp != null)
            {
                empleado = new Empleado_Poco()
                {
                    Id_Empleado = emp.Id_Empleado,
                    Id_Estado = emp.Id_Estado,
                    Id_Persona = emp.Id_Persona,
                };
            }
            Proveedor_Poco proveedor = null;
            if (pro != null)
            {
                proveedor = new Proveedor_Poco()
                {
                    Id_Proveedor = pro.Id_Proveedor,
                    Id_Estado = pro.Id_Estado,
                    Id_Persona = pro.Id_Persona,
                    Nombre_Empresa = pro.Nombre_Empresa,
                    Telefono_Empresa = pro.Telefono_Empresa,
                    Direccion_Empresa = pro.Direccion_Empresa
                };
            }


            Tipo_Persona tipo_persona = new Tipo_Persona()
            {
                _persona = persona,
                _cliente = cliente,
                _empleado = empleado,
                _proveedor = proveedor

            };
            if (tipo_persona == null)
            {
                return HttpNotFound();
            }

            return View(tipo_persona);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos de la Persona.";
                return View();
            }
        }

        //GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Personas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tipo_Persona tipo_persona)
        {
            try { 
            if (ModelState.IsValid)
            {
                Persona personaObj = new Persona
                {
                    Id_Persona = tipo_persona._persona.Id_Persona,
                    Nombre = tipo_persona._persona.Nombre,
                    Apellidos = tipo_persona._persona.Apellidos,
                    Direccion = tipo_persona._persona.Direccion,
                    Telefono = tipo_persona._persona.Telefono,
                    Id_Estado = 1,
                    Chk_Cliente = tipo_persona._persona.Chk_Cliente,
                    Chk_Empleado = tipo_persona._persona.Chk_Empleado,
                    Chk_Proveedor = tipo_persona._persona.Chk_Proveedor
                };
                db.Persona.Add(personaObj);
                db.SaveChanges();
                if (tipo_persona._persona.Chk_Cliente)
                {
                    Cliente cli = new Cliente
                    {
                        Id_Persona = personaObj.Id_Persona,
                        Id_Estado = 1
                    };
                    db.Cliente.Add(cli);
                    db.SaveChanges();
                }
                if (tipo_persona._persona.Chk_Proveedor)
                {
                    Proveedor pro = new Proveedor
                    {
                        Id_Persona = personaObj.Id_Persona,
                        Id_Estado = 1,
                        Nombre_Empresa = tipo_persona._proveedor.Nombre_Empresa,
                        Telefono_Empresa = tipo_persona._proveedor.Telefono_Empresa,
                        Direccion_Empresa = tipo_persona._proveedor.Direccion_Empresa
                    };
                    db.Proveedor.Add(pro);
                    db.SaveChanges();
                }
                if (tipo_persona._persona.Chk_Empleado)
                {
                    Empleado emp = new Empleado
                    {
                        Id_Persona = personaObj.Id_Persona,
                        Id_Estado = 1
                    };
                    db.Empleado.Add(emp);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(tipo_persona);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al crear la Persona.";
                return View();
            }
        }

        //GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            try{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona per = db.Persona.FirstOrDefault(x => x.Id_Persona == id);
            Cliente cli = db.Cliente.FirstOrDefault(x => x.Id_Persona == id);
            Empleado emp = db.Empleado.FirstOrDefault(x => x.Id_Persona == id);
            Proveedor pro = db.Proveedor.FirstOrDefault(x => x.Id_Persona == id);
            Persona_Poco persona = new Persona_Poco() 
            {
                Id_Persona = per.Id_Persona,
                Nombre = per.Nombre,
                Apellidos = per.Apellidos,
                Direccion = per.Direccion,
                Telefono = per.Telefono,
                Id_Estado = per.Id_Estado,
                Chk_Cliente = per.Chk_Cliente,
                Chk_Empleado =per.Chk_Empleado,
                Chk_Proveedor = per.Chk_Proveedor
            };
            Cliente_Poco cliente = null;
            if (cli != null)
            {
                cliente = new Cliente_Poco()
                {
                    Id_Cliente = cli.Id_Cliente,
                    Id_Estado = cli.Id_Estado,
                    Id_Persona = cli.Id_Persona,
                };
            }
            Empleado_Poco empleado = null;
            if (emp != null)
            {
                empleado = new Empleado_Poco()
                {
                    Id_Empleado = emp.Id_Empleado,
                    Id_Estado = emp.Id_Estado,
                    Id_Persona = emp.Id_Persona,
                };
            }
            Proveedor_Poco proveedor = null;
            if (pro != null)
            {
                proveedor = new Proveedor_Poco()
            {
                Id_Proveedor=pro.Id_Proveedor,
                Id_Estado = pro.Id_Estado,
                Id_Persona = pro.Id_Persona,
                Nombre_Empresa = pro.Nombre_Empresa,
                Telefono_Empresa = pro.Telefono_Empresa,
                Direccion_Empresa = pro.Direccion_Empresa
            };
            }


            Tipo_Persona tipo_persona = new Tipo_Persona()
            {
                _persona = persona,
                _cliente=cliente,
                _empleado=empleado,
                _proveedor=proveedor
                
            };
            if (tipo_persona == null)
            {
                return HttpNotFound();
            }

            return View(tipo_persona);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos de la Persona.";
                return View();
            }
        }

        // POST: Personas/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_persona"></param>
        /// <returns>Un View con un tipo_persona por parametros</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Edit(Tipo_Persona tipo_persona)
        {
            //if (ModelState.IsValid)
            //{
            try { 
                Persona persona = db.Persona.FirstOrDefault(x => x.Id_Persona == tipo_persona._persona.Id_Persona);
                    persona.Id_Persona = tipo_persona._persona.Id_Persona;
                    persona.Nombre = tipo_persona._persona.Nombre;
                    persona.Apellidos = tipo_persona._persona.Apellidos;
                    persona.Direccion = tipo_persona._persona.Direccion;
                    persona.Telefono = tipo_persona._persona.Telefono;
                    persona.Id_Estado = 1;
                    persona.Chk_Cliente = tipo_persona._persona.Chk_Cliente;
                    persona.Chk_Empleado = tipo_persona._persona.Chk_Empleado;
                    persona.Chk_Proveedor = tipo_persona._persona.Chk_Proveedor;
                
                db.Entry(persona).State = EntityState.Modified;
                Cliente cliente = db.Cliente.FirstOrDefault(x=>x.Id_Persona==tipo_persona._persona.Id_Persona);
                if ((cliente!=null)&&(!tipo_persona._persona.Chk_Cliente))
                {
                        cliente.Id_Persona = tipo_persona._persona.Id_Persona;
                        cliente.Id_Estado = 2;
                    
                    db.Entry(cliente).State = EntityState.Modified;

                }
                else if ((cliente == null) && (tipo_persona._persona.Chk_Cliente))
                {
                    cliente = new Cliente()
                    {
                        Id_Persona = tipo_persona._persona.Id_Persona,
                        Id_Estado = 1
                    };
                    db.Cliente.Add(cliente);
                }
                Proveedor proveedor = db.Proveedor.FirstOrDefault(x => x.Id_Persona == tipo_persona._persona.Id_Persona);
                if ((proveedor!=null)&&(!tipo_persona._persona.Chk_Proveedor))
                {
                        proveedor.Id_Persona = persona.Id_Persona;
                        proveedor.Id_Estado = 2;
                        proveedor.Nombre_Empresa = tipo_persona._proveedor.Nombre_Empresa;
                        proveedor.Telefono_Empresa = tipo_persona._proveedor.Telefono_Empresa;
                        proveedor.Direccion_Empresa = tipo_persona._proveedor.Direccion_Empresa;
                    db.Entry(proveedor).State = EntityState.Modified;
                }
                else if ((proveedor == null) && (tipo_persona._persona.Chk_Proveedor))
                {
                    proveedor = new Proveedor
                    {
                        Id_Persona = persona.Id_Persona,
                        Id_Estado = 1,
                        Nombre_Empresa = tipo_persona._proveedor.Nombre_Empresa,
                        Telefono_Empresa = tipo_persona._proveedor.Telefono_Empresa,
                        Direccion_Empresa = tipo_persona._proveedor.Direccion_Empresa
                    };
                    db.Proveedor.Add(proveedor);
                }
                else if ((proveedor != null) && (tipo_persona._persona.Chk_Proveedor))
                {
                        proveedor.Id_Persona = persona.Id_Persona;
                        proveedor.Id_Estado = 1;
                        proveedor.Nombre_Empresa = tipo_persona._proveedor.Nombre_Empresa;
                        proveedor.Telefono_Empresa = tipo_persona._proveedor.Telefono_Empresa;
                        proveedor.Direccion_Empresa = tipo_persona._proveedor.Direccion_Empresa;
                    db.Entry(proveedor).State = EntityState.Modified;
                }
                Empleado empleado = db.Empleado.FirstOrDefault(x => x.Id_Persona == tipo_persona._persona.Id_Persona);
                if ((empleado!=null)&&(!tipo_persona._persona.Chk_Empleado))
                {
                    empleado.Id_Persona = tipo_persona._persona.Id_Persona;
                    empleado.Id_Estado = 2;
                    db.Entry(empleado).State = EntityState.Modified;
                }
                else if ((empleado == null) && (tipo_persona._persona.Chk_Empleado))
                {
                    empleado = new Empleado
                    {
                        Id_Persona = tipo_persona._persona.Id_Persona,
                        Id_Estado = 1
                    };
                    db.Empleado.Add(empleado);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            //}

            //return View(tipo_persona);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al editar la Persona.";
                return View();
            }
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona per = db.Persona.FirstOrDefault(x => x.Id_Persona == id);
            Cliente cli = db.Cliente.FirstOrDefault(x => x.Id_Persona == id);
            Empleado emp = db.Empleado.FirstOrDefault(x => x.Id_Persona == id);
            Proveedor pro = db.Proveedor.FirstOrDefault(x => x.Id_Persona == id);
            Persona_Poco persona = new Persona_Poco()
            {
                Id_Persona = per.Id_Persona,
                Nombre = per.Nombre,
                Apellidos = per.Apellidos,
                Direccion = per.Direccion,
                Telefono = per.Telefono,
                Id_Estado = per.Id_Estado,
                Chk_Cliente = per.Chk_Cliente,
                Chk_Empleado = per.Chk_Empleado,
                Chk_Proveedor = per.Chk_Proveedor
            };
            Cliente_Poco cliente = null;
            if (cli != null)
            {
                cliente = new Cliente_Poco()
                {
                    Id_Cliente = cli.Id_Cliente,
                    Id_Estado = cli.Id_Estado,
                    Id_Persona = cli.Id_Persona,
                };
            }
            Empleado_Poco empleado = null;
            if (emp != null)
            {
                empleado = new Empleado_Poco()
                {
                    Id_Empleado = emp.Id_Empleado,
                    Id_Estado = emp.Id_Estado,
                    Id_Persona = emp.Id_Persona,
                };
            }
            Proveedor_Poco proveedor = null;
            if (pro != null)
            {
                proveedor = new Proveedor_Poco()
                {
                    Id_Proveedor = pro.Id_Proveedor,
                    Id_Estado = pro.Id_Estado,
                    Id_Persona = pro.Id_Persona,
                    Nombre_Empresa = pro.Nombre_Empresa,
                    Telefono_Empresa = pro.Telefono_Empresa,
                    Direccion_Empresa = pro.Direccion_Empresa
                };
            }
            Tipo_Persona tipo_persona = new Tipo_Persona()
            {
                _persona = persona,
                _cliente = cliente,
                _empleado = empleado,
                _proveedor = proveedor

            };
            if (tipo_persona == null)
            {
                return HttpNotFound();
            }
            return View(tipo_persona);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los datos de la Persona.";
                return View();
            }
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Persona persona = db.Persona.FirstOrDefault(x => x.Id_Persona == id);
            if (persona!=null)
            {
                if (persona.Id_Estado == 1)
                {
                    persona.Id_Estado = 2;
                    db.Entry(persona).State = EntityState.Modified;
                }
            }
            
            Cliente cliente = db.Cliente.FirstOrDefault(x => x.Id_Persona == id);
            if (cliente != null)
            {
                if (cliente.Id_Estado == 1)
                {
                    persona.Id_Estado = 2;
                    db.Entry(cliente).State = EntityState.Modified;
                }
            }
            Empleado empleado = db.Empleado.FirstOrDefault(x => x.Id_Persona == id);
            if (empleado != null)
            {
                if (empleado.Id_Estado == 1)
                {
                    empleado.Id_Estado = 2;
                    db.Entry(empleado).State = EntityState.Modified;
                }
            }
            Proveedor proveedor = db.Proveedor.FirstOrDefault(x => x.Id_Persona == id);
            if (proveedor!=null)
            {
                if (proveedor.Id_Estado == 1)
                {
                    proveedor.Id_Estado = 2;
                    db.Entry(proveedor).State = EntityState.Modified;
                }
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al eliminar la Persona.";
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
        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), "Empleados.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            var data = (from p in db.Persona.Where(x=>x.Chk_Empleado==true)
                        from e in db.Empleado.Where(x => x.Id_Persona == p.Id_Persona).DefaultIfEmpty()
                        select new
                        {
                            Nombre = p.Nombre,
                            Apellidos = p.Apellidos,
                            Direccion = p.Direccion,
                            Telefono = p.Telefono,
                        }).ToList();
            ReportDataSource rd = new ReportDataSource("dsPersona", data);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }
    }
}

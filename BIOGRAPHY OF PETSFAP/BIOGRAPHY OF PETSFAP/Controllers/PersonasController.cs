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

namespace BIOGRAPHY_OF_PETSFAP.Controllers
{

    public class PersonasController : Controller
    {
        private IEnumerable<Persona> ListaPersona;
        //private IEnumerable<Tipo_Persona> ListaTipoPersona;
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Personas
        public ActionResult Index()
        {

            ListaPersona = db.Persona.Include(c => c.Cliente).Include(c => c.Estado).Include(c => c.Empleado).Include(c => c.Proveedor).Where(x => x.Id_Estado == 1);
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            return View(ListaPersona);
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
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

        //GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
            List<Persona> persona = new List<Persona>();
            persona = db.Persona.Include(x => x.Empleado).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "Empleados.rpt"));
            rd.SetDataSource(persona);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                Response.AddHeader("Content-Disposition", "inline; filename=Empleados.pdf");
                return File(stream, "application/pdf");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

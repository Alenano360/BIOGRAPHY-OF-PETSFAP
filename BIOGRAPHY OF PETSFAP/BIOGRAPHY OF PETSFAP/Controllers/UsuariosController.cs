using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BIOGRAPHY_OF_PETSFAP.Models;
using System.Web.Security;

namespace BIOGRAPHY_OF_PETSFAP.Controllers
{
    public class UsuariosController : Controller
    {
        string RolUsuario;
        private VeterinariaEntities db = new VeterinariaEntities();
        public ActionResult Index()
        {
            try
            {
                ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
                var usuarios = db.Usuarios.Include(u => u.Empleado).Include(u => u.Estado).Include(u => u.Roles).Where(x => x.Id_Estado == 1);
                return View(usuarios.ToList());
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los Usuarios.";
                return View();
            }

        }
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuarios usuarios = db.Usuarios.Find(id);
                if (usuarios == null)
                {
                    return HttpNotFound();
                }
                return View(usuarios);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al cargar los Usuarios.";
                return View();
            }
        }
        public ActionResult Create()
        {
            ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto");
            ViewBag.Id_Rol = new SelectList(db.Roles.Where(x => x.Id_Estado == 1), "Id_Rol", "Descripcion");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Usuario,Id_Empleado,Correo,Usuario,Contraseña,Id_Rol,Id_Estado")] Usuarios usuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuarios.Id_Estado = 1;
                    db.Usuarios.Add(usuarios);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", usuarios.Id_Empleado);
                ViewBag.Id_Rol = new SelectList(db.Roles.Where(x => x.Id_Estado == 1), "Id_Rol", "Descripcion", usuarios.Id_Rol);
                return View(usuarios);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al crear el Usuario.";
                return View();
            }
        }
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuarios usuarios = db.Usuarios.Find(id);
                if (usuarios == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", usuarios.Id_Empleado);
                ViewBag.Id_Rol = new SelectList(db.Roles.Where(x => x.Id_Estado == 1), "Id_Rol", "Descripcion", usuarios.Id_Rol);
                return View(usuarios);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al editar el Usuario.";
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Usuario,Id_Empleado,Correo,Usuario,Contraseña,Id_Rol,Id_Estado")] Usuarios usuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuarios.Id_Estado = 1;
                    db.Entry(usuarios).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Id_Empleado = new SelectList(db.Empleado.Where(x => x.Id_Estado == 1), "Id_Empleado", "NombreCompleto", usuarios.Id_Empleado);
                ViewBag.Id_Rol = new SelectList(db.Roles.Where(x => x.Id_Estado == 1), "Id_Rol", "Descripcion", usuarios.Id_Rol);
                return View(usuarios);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al editar el Usuario.";
                return View();
            }
        }
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuarios usuarios = db.Usuarios.Find(id);
                if (usuarios == null)
                {
                    return HttpNotFound();
                }
                return View(usuarios);
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al editar el Usuario.";
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Usuarios usuarios = db.Usuarios.Find(id);
                if (usuarios.Id_Estado == 1)
                {
                    usuarios.Id_Estado = 2;
                    db.Entry(usuarios).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Exception = "Error al editar el Usuario.";
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Usuarios user)
        {
            try
            {
                if (IsValid(user.Usuario, user.Contraseña))
                {
                    FormsAuthentication.SetAuthCookie(user.Usuario, false);
                    if (RolUsuario.Equals("Administrador"))
                    {
                        return RedirectToAction("Index", "Usuarios");
                    }
                    else if (RolUsuario.Equals("Cajero"))
                    {
                        return RedirectToAction("Index", "Facturas");
                    }
                    else if (RolUsuario.Equals("Doctor"))
                    {
                        return RedirectToAction("Index", "Pacientes");
                    }
                    else if (RolUsuario.Equals("Inventario"))
                    {
                        return RedirectToAction("Index", "Productoes");
                    }
                }
                else
                {
                    ViewBag.Error = "Login Fallido, Compruebe su Usuario o Contraseña";
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrio un Error en el Sistema...";
            }
            return View(user);
        }
        private bool IsValid(string usuario, string contrasena)
        {
            try
            {
                bool IsValid = false;

                using (var db = new BIOGRAPHY_OF_PETSFAP.Models.VeterinariaEntities())
                {
                    var user = db.Usuarios.FirstOrDefault(u => u.Usuario == usuario && u.Contraseña == contrasena);

                    if (user != null)
                    {
                        Session.Add("NombreUsuarioSession", user.Empleado.Persona.Nombre + " " + user.Empleado.Persona.Apellidos);
                        Session.Add("RolUsuarioSession", user.Roles.Descripcion);
                        IsValid = true;
                    }
                    if (IsValid == true)
                    {
                        if (user.Roles.Descripcion.Equals("Administrador"))
                        {
                            RolUsuario = user.Roles.Descripcion;
                        }
                        else if (user.Roles.Descripcion.Equals("Cajero"))
                        {
                            RolUsuario = user.Roles.Descripcion;
                        }
                        else if (user.Roles.Descripcion.Equals("Doctor"))
                        {
                            RolUsuario = user.Roles.Descripcion;
                        }
                        else if (user.Roles.Descripcion.Equals("Inventario"))
                        {
                            RolUsuario = user.Roles.Descripcion;
                        }
                    }
                }

                return IsValid;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
        public void DestroySession()
        {
            Session["RolUsuarioSession"] = null;
            Session["NombreUsuarioSession"] = null;
        }
        public ActionResult DestroySessionRedirect()
        {
            Session["RolUsuarioSession"] = null;
            Session["NombreUsuarioSession"] = null;
            return View("Login");
        }
    }
}

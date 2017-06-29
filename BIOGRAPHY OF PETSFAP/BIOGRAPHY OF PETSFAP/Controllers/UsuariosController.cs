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
        private VeterinariaEntities db = new VeterinariaEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Empleado).Include(u => u.Estado).Include(u => u.Roles).Where(x => x.Id_Estado == 1);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
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

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.Id_Empleado = new SelectList(db.Empleado, "Id_Empleado", "Id_Empleado");
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion");
            ViewBag.Id_Rol = new SelectList(db.Roles, "Id_Rol", "Descripcion");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Usuario,Id_Empleado,Correo,Usuario,Contraseña,Id_Rol,Id_Estado")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Id_Estado = 1;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Empleado = new SelectList(db.Empleado, "Id_Empleado", "Id_Empleado", usuarios.Id_Empleado);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", usuarios.Id_Estado);
            ViewBag.Id_Rol = new SelectList(db.Roles, "Id_Rol", "Descripcion", usuarios.Id_Rol);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Id_Empleado = new SelectList(db.Empleado, "Id_Empleado", "Id_Empleado", usuarios.Id_Empleado);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", usuarios.Id_Estado);
            ViewBag.Id_Rol = new SelectList(db.Roles, "Id_Rol", "Descripcion", usuarios.Id_Rol);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Usuario,Id_Empleado,Correo,Usuario,Contraseña,Id_Rol,Id_Estado")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Id_Estado = 1;
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Empleado = new SelectList(db.Empleado, "Id_Empleado", "Id_Empleado", usuarios.Id_Empleado);
            ViewBag.Id_Estado = new SelectList(db.Estado, "Id_Estado", "Descripcion", usuarios.Id_Estado);
            ViewBag.Id_Rol = new SelectList(db.Roles, "Id_Rol", "Descripcion", usuarios.Id_Rol);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Fallido, Compruebe su Usuario o Contraseña :)");
                }
            }catch(Exception ex){
                ModelState.AddModelError("", "Ocurrio un Error...");
            } 
            return View(user);
        }

        private bool IsValid(string usuario, string contrasena)
        {
            bool IsValid = false;
            
            using (var db = new BIOGRAPHY_OF_PETSFAP.Models.VeterinariaEntities())
            {
                var user = db.Usuarios.FirstOrDefault(u => u.Usuario == usuario && u.Contraseña==contrasena);
                Session.Add("NombreUsuarioSession", user.Empleado.Persona.Nombre + " " + user.Empleado.Persona.Apellidos);
                Session.Add("RolUsuarioSession", user.Roles.Descripcion);
                if (user != null)
                {
                        IsValid = true;
                }
            }
            
            return IsValid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIOGRAPHY_OF_PETSFAP.Models;
using System.ComponentModel.DataAnnotations;

namespace BIOGRAPHY_OF_PETSFAP.Class
{
    public class Persona_Poco
    {
        public int Id_Persona { get; set; }
        [Required(ErrorMessage = "El campo de Nombre es requerido")]
        [DataType(DataType.Text,ErrorMessage="El campo solo puede llevar texto")]
        [Display(Name = "Nombre")] 
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo de Apellidos es requerido")]
        [DataType(DataType.Text, ErrorMessage = "El campo solo puede llevar texto")]
        [Display(Name = "Apellidos")] 
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El campo de Direccion es requerido")]
        [DataType(DataType.Text, ErrorMessage = "El campo solo puede llevar texto")]
        [Display(Name = "Direccion")] 
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo de Telefono es requerido")]
        [DataType(DataType.Text, ErrorMessage = "El campo solo puede llevar texto")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        public int Id_Estado { get; set; }
        public bool Chk_Cliente { get; set; }
        public bool Chk_Empleado { get; set; }
        public bool Chk_Proveedor { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }

    public partial class Proveedor_Poco
    {
        public Proveedor_Poco()
        {
            this.Factura = new HashSet<Factura>();
            this.Producto = new HashSet<Producto>();
        }

        public int Id_Proveedor { get; set; }
        public int Id_Persona { get; set; }
        public int Id_Estado { get; set; }
        [Required(ErrorMessage = "El campo de Nombre Empresa es requerido")]
        [DataType(DataType.Text, ErrorMessage = "El campo solo puede llevar texto")]
        [Display(Name = "Nombre_Empresa")]
        public string Nombre_Empresa { get; set; }
        [Required(ErrorMessage = "El campo de Telefono Empresa es requerido")]
        [DataType(DataType.Text, ErrorMessage = "El campo solo puede llevar texto")]
        [Display(Name = "Telefono_Empresa")]
        public string Telefono_Empresa { get; set; }
        [Required(ErrorMessage = "El campo de Direccion_Empresa es requerido")]
        [DataType(DataType.Text, ErrorMessage = "El campo solo puede llevar texto")]
        [Display(Name = "Direccion_Empresa")]
        public string Direccion_Empresa { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual Persona_Poco Persona_Poco { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }

    public partial class Cliente_Poco
    {
        public Cliente_Poco()
        {
            this.Cita_Medica = new HashSet<Cita_Medica>();
            this.Factura = new HashSet<Factura>();
            this.Paciente = new HashSet<Paciente>();
        }

        public int Id_Cliente { get; set; }
        public int Id_Persona { get; set; }
        public int Id_Estado { get; set; }

        public virtual ICollection<Cita_Medica> Cita_Medica { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<Paciente> Paciente { get; set; }
        public virtual Persona Persona { get; set; }
    }

    public partial class Empleado_Poco
    {
        public Empleado_Poco()
        {
            this.Factura = new HashSet<Factura>();
            this.Usuarios = new HashSet<Usuarios>();
        }

        public int Id_Empleado { get; set; }
        public int Id_Persona { get; set; }
        public int Id_Estado { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual Persona Persona_Poco { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
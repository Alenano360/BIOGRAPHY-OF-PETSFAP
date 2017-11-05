using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIOGRAPHY_OF_PETSFAP.Models;
using System.ComponentModel.DataAnnotations;

namespace BIOGRAPHY_OF_PETSFAP.Class
{
    public class Cita_Poco
    {
        public Cita_Poco()
        {
            this.Detalle_Medicina_Poco = new HashSet<Detalle_Medicina_Poco>();
            this.Detalle_Servicio_Poco = new HashSet<Detalle_Servicio_Poco>();
        }

        public int Id_Cita { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.TimeSpan Hora_Inico { get; set; }
        public System.TimeSpan Hora_Final { get; set; }
        public string Estado_Cita { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Empleado { get; set; }
        public int Id_Paciente { get; set; }
        public string Descripcion { get; set; }
        public int Costo_Total { get; set; }
        public int Id_Estado { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Detalle_Medicina_Poco> Detalle_Medicina_Poco { get; set; }
        public virtual ICollection<Detalle_Servicio_Poco> Detalle_Servicio_Poco { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Paciente Paciente { get; set; }
    }

    public partial class Detalle_Medicina_Poco
    {
        public int Id_Detalle_Medicina { get; set; }
        public int Id_Cita { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
        public int Precio_Total { get; set; }

        public virtual Cita Cita { get; set; }
        public virtual Producto Producto { get; set; }
    }

    public partial class Detalle_Servicio_Poco
    {
        public int Id_Detalle_Servicio { get; set; }
        public int Id_Cita { get; set; }
        public int Id_Servicio { get; set; }
        public int Cantidad { get; set; }
        public int Precio_Total { get; set; }

        public virtual Cita Cita { get; set; }
        public virtual Servicio Servicio { get; set; }
    }

    public partial class Producto_Poco_Cita
    {
        public Producto_Poco_Cita()
        {
            this.Detalle_Medicina_Poco = new HashSet<Detalle_Medicina_Poco>();
        }

        public int Id_Producto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public int Id_Proveedor { get; set; }
        public int Id_Categoria { get; set; }
        public int Id_Estado { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Detalle_Medicina_Poco> Detalle_Medicina_Poco { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
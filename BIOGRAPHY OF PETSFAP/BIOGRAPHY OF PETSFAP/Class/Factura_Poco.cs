using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIOGRAPHY_OF_PETSFAP.Models;
using System.ComponentModel.DataAnnotations;

namespace BIOGRAPHY_OF_PETSFAP.Class
{
    public class Factura_Poco
    {

            public Factura_Poco()
            {
                this.Detalle_Factura = new HashSet<Detalle_Factura_Poco>();
            }

            public int Numero_Factura { get; set; }
            [Required(ErrorMessage = "El campo de Empleado es requerido")]
            [Display(Name = "Empleado")]
            public int Id_Empleado { get; set; }
            [Required(ErrorMessage = "El campo de Cliente es requerido")]
            [Display(Name = "Cliente")]
            public Nullable<int> Id_Cliente { get; set; }
            [Required(ErrorMessage = "El campo de Proveedor es requerido")]
            [Display(Name = "Proveedor")]
            public Nullable<int> Id_Proveedor { get; set; }
            [Required(ErrorMessage = "El campo de Fecha es requerido")]
            [DataType(DataType.DateTime)]
            [Display(Name = "Fecha")]
            public System.DateTime Fecha { get; set; }
            [Required(ErrorMessage = "El campo de Precio Total es requerido")]
            [Display(Name = "Precio_Total")]
            public int Precio_Total { get; set; }
            public int Id_Estado { get; set; }

            public virtual Cliente Cliente { get; set; }
            public virtual ICollection<Detalle_Factura_Poco> Detalle_Factura { get; set; }
            public virtual Empleado Empleado { get; set; }
            public virtual Estado Estado { get; set; }
            public virtual Proveedor Proveedor { get; set; }
      }

        public class Detalle_Factura_Poco
        {

                public int Id_Detalle { get; set; }
                public int Numero_Factura { get; set; }
                public int Id_Producto { get; set; }
                public int Cantidad { get; set; }
                public int Precio_Total_Producto { get; set; }
                public int Precio_Unitario { get; set; }

                public virtual Factura_Poco Factura { get; set; }
                public virtual Producto Producto { get; set; }
            }

        public class Producto_Poco
        {
            public Producto_Poco()
            {
                this.Detalle_Factura = new HashSet<Detalle_Factura>();
            }

            public int Id_Producto { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public double Precio { get; set; }
            public int Cantidad { get; set; }
            public int Id_Proveedor { get; set; }
            public int Id_Estado { get; set; }

            public virtual ICollection<Detalle_Factura> Detalle_Factura { get; set; }
            public virtual Estado Estado { get; set; }
            public virtual Proveedor Proveedor { get; set; }
        }
}
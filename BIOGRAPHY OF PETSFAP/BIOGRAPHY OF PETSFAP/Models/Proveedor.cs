//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIOGRAPHY_OF_PETSFAP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proveedor
    {
        public Proveedor()
        {
            this.Factura = new HashSet<Factura>();
            this.Producto = new HashSet<Producto>();
        }
    
        public int Id_Proveedor { get; set; }
        public int Id_Persona { get; set; }
        public int Id_Estado { get; set; }
        public string Nombre_Empresa { get; set; }
        public string Telefono_Empresa { get; set; }
        public string Direccion_Empresa { get; set; }
    
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}

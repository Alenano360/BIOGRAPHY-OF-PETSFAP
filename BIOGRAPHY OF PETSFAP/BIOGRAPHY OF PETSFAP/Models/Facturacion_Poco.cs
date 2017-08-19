using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIOGRAPHY_OF_PETSFAP.Class;

namespace BIOGRAPHY_OF_PETSFAP.Models
{
    public class Facturacion_Poco
    {
        public Factura_Poco _factura { get; set; }
        public List<Detalle_Factura_Poco> _detalle { get; set; }
        public List<Producto_Poco> _producto { get; set; }

    }
}
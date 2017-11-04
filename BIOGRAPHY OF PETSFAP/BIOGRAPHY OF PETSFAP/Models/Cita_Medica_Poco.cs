using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIOGRAPHY_OF_PETSFAP.Class;
namespace BIOGRAPHY_OF_PETSFAP.Models
{
    public class Cita_Medica_Poco
    {
        public Cita_Poco _cita { get; set; }
        public Detalle_Medicina_Poco _detalle_medicina { get; set; }
        public Detalle_Servicio_Poco _detalle_servicio { get; set; }
        public Producto_Poco_Cita _producto { get; set; }
    }
}
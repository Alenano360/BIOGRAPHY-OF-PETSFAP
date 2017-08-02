using BIOGRAPHY_OF_PETSFAP.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIOGRAPHY_OF_PETSFAP.Models
{
    public class Tipo_Persona
    {
        public Persona_Poco _persona { get; set; }
        public Proveedor_Poco _proveedor { get; set; }
        public Cliente_Poco _cliente { get; set; }
        public Empleado_Poco _empleado { get; set; }
    }

}
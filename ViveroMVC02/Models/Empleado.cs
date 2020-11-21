using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViveroMVC02.Models
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string NroDocumento { get; set; }
        public int ProvinciaId { get; set; }
        public int LocalidadId { get; set; }
        public Provincia Provincia { get; set; }
        public Localidad Localidad { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string CorreoElectronico { get; set; }


    }
}
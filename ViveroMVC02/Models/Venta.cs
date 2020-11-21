using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViveroMVC02.Models
{
    public class Venta
    {
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Total { get; set; }



    }
}
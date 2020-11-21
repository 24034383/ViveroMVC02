using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViveroMVC02.Models
{
    public class Compra
    {
        public int CompraId { get; set; }
        public DateTime Fecha { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public decimal Total { get; set; }
       


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViveroMVC02.Models
{
    public class Kardex
    {
        public int KardexId { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public DateTime Fecha { get; set; }
        public string Movimiento { get; set; }

        public int Entrada { get; set; }
        public int Salida { get; set; }
        public int Saldo { get; set; }
        public decimal UltimoCosto { get; set; }
        public decimal CostoPromedio { get; set; }




    }
}
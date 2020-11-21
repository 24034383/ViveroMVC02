using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViveroMVC02.Models
{
    public class ItemCompra
    {
        public int ItemCompraId { get; set; }

        public int CompraId { get; set; }
        public Compra Compra { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public int KardexId { get; set; }

        public Kardex Kardex { get; set; }



    }
}
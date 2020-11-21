using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.ItemCompra
{
    public class ItemCompraListViewModels
    {



        [Display(Name = "Nro de Item")]
        public int ItemCompraId { get; set; }

        [Display(Name = "Nro de Compra")]
        public int CompraId { get; set; }
        public String Compra { get; set; }

        [Display(Name = "Proveedor")]
        public String Proveedor { get; set; }

        [Display(Name = "Producto")]
        public String Producto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }


    }
}
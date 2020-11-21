using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ViveroMVC02.Models;

namespace ViveroMVC02.ViewModels.Producto
{
    public class ProductoListViewModels
    {
        public int ProductoId { get; set; }

        [Display(Name = "Producto")]
        public string NombreProducto { get; set; }
        public string Categoria { get; set; }
        public string Proveedor { get; set; }

        [Display(Name = "Stock")]
        public decimal UnidadesEnStock { get; set; }

        [Display(Name = "Precio")]
        public decimal PrecioVenta { get; set; }
        public bool Suspendido { get; set; }




    }
}
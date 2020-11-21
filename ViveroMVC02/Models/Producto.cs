using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        [Display(Name = "Producto")]
        public string NombreProducto { get; set; }
        [Display(Name = "Proveedor")]
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        [Display(Name = "Stock")]
        public int UnidadesEnStock { get; set; }
        [Display(Name = "Pedidos")]
        public int UnidadesEnPedido { get; set; }
        [Display(Name = "Reposicion")]
        public int NivelDeReposicion { get; set; }
        [Display(Name = "Precio Costo")]
        public decimal PrecioCosto { get; set; }

        [Display(Name = "Precio Venta")]
        public decimal PrecioVenta { get; set; }
        public bool Suspendido { get; set; }
      




    }
}
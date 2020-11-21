using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ViveroMVC02.Models;

namespace ViveroMVC02.ViewModels.Producto
{
    public class ProductoEditViewModels
    {

        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = "Producto")]

        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un proveedor")]
        [Display(Name = @"Proveedor")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
        [Display(Name = @"Categoría")]

        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = @"Precio Vta.")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo {0} puede tomar valores entre {1} y {2}")]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = @"Precio Cto.")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo {0} puede tomar valores entre {1} y {2}")]
        public decimal PrecioCosto { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = @"Stock")]
        [Range(0, 100, ErrorMessage = "El campo {0} puede tomar valores entre {1} y {2}")]
        public decimal UnidadesEnStock { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = @"UnidadesEnPedido")]
        [Range(0, 100, ErrorMessage = "El campo {0} puede tomar valores entre {1} y {2}")]
        public decimal UnidadesEnPedido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = @"UnidadesEnReposicion")]
        [Range(0, 100, ErrorMessage = "El campo {0} puede tomar valores entre {1} y {2}")]
        public decimal UnidadesEnReposicion { get; set; }
        public bool Suspendido { get; set; }

        public List<Models.Proveedor> Proveedores { get; set; }
        public List<Models.Categoria> Categorias { get; set; }

        


    }
}
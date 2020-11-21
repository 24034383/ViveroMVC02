using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.ItemCompra
{
    public class ItemCompraEditViewModels
    {



        public int ItemCompraId { get; set; }


        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Proveedor")]
        //[Display(Name = @"Proveedor")]
        //public int ProveedorId { get; set; }
        //public List<Models.Proveedor> Proveedor { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Producto")]
        [Display(Name = @"Producto")]
        public int ProductoId { get; set; }
        public List<Models.Producto> Productos { get; set; }


        //public List<Models.ItemCompra> ItemCompras { get; set; }

        //public List<Models.Kardex> Kardices { get; set; }

        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //[DataType(DataType.DateTime)]
        //[MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        //[Display(Name = @"Fecha")]

        //public DateTime Fecha { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"PrecioUnitario")]
        public decimal PrecioUnitario  { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Total")]
        public decimal Total { get; set; }


    }
}
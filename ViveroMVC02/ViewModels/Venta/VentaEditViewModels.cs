using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Venta
{
    public class VentaEditViewModels
    {

        public int VentaId { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Provincia")]
        [Display(Name = @"Cliente")]
        public int ClienteId { get; set; }


        public List<Models.Cliente> Cliente { get; set; }




        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.DateTime)]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Fecha")]

        public DateTime Fecha { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Total")]
        public decimal Total { get; set; }







    }
}
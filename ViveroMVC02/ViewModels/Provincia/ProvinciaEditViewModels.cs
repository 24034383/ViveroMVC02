using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Provincia
{
    public class ProvinciaEditViewModels
    {
        public int ProvinciaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} and {1} caracteres", MinimumLength = 3)]
        [Display(Name = @"Provincia")]
        public string  NombreProvincia { get; set; }

    }
}
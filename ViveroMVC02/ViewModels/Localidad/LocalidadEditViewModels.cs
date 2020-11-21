using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Localidad
{
    public class LocalidadEditViewModels
    {
        public int LocalidadId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} and {1} caracteres", MinimumLength = 3)]
        [Display(Name = @"Localidad")]

        public string NombreLocalidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Provincia")]
        [Display(Name = @"Provincia")]
        public int ProvinciaId { get; set; }
        public List<Models.Provincia> Provincias { get; set; }


    }
}
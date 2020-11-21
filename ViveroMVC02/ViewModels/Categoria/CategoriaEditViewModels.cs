using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Categoria
{
    public class CategoriaEditViewModels
    {
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} and {1} caracteres", MinimumLength = 3)]
        [Display(Name = @"Categoría")]
        public string NombreCategoria { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Descripción")]
        public string Descripcion { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Categoria
{
    public class CategoriaListViewModels
    {

        public int CategoriaId { get; set; }

        [Display(Name = @"Categoría")]
        public string NombreCategoria { get; set; }

        public string Descripcion { get; set; }
    }
}
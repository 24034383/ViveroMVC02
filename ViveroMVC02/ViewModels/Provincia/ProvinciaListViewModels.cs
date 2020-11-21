using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Provincia
{
    public class ProvinciaListViewModels
    {


        public int ProvinciaId { get; set; }

        [Display(Name = @"Provincia")]
        public string NombreProvincia { get; set; }


    }
}
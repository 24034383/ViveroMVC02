using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Proveedor
{
    public class ProveedorListViewModels
    {
        public int ProveedorId { get; set; }

        [Display(Name = "Razon Social")]
        public string RazonSocial { get; set; }
        public String Cuit { get; set; }


        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        public string Localidad { get; set; }





    }
}
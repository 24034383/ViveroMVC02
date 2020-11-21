using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Compra
{
    public class CompraListViewModels
    {


        [Display(Name = "Nro de Compra")]
        public int CompraId { get; set; }

        [Display(Name = "Proveedor")]
        public String Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

       




    }
}
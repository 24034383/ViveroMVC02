using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Venta
{
    public class VentaListViewModels
    {



        [Display(Name = "Nro de Venta")]
        public int VentaId { get; set; }

        [Display(Name = "Cliente")]
        public String Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }



    }
}
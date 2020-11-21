﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Empleado
{
    public class EmpleadoListViewModels
    {

        public int EmpleadoId { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        public string Localidad { get; set; }

    }
}
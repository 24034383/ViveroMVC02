using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels.Cliente
{
    public class ClienteEditViewModels
    {

        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe contener entre {2} and {1} caracteres", MinimumLength = 3)]
        [Display(Name = @"Nombre")]
        public string Nombre{ get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe contener entre {2} and {1} caracteres", MinimumLength = 3)]
        [Display(Name = @"Apellido")]
        public string Apellido { get; set; }


        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Dirección")]
        public string Direccion { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Provincia")]
        [Display(Name = @"Provincia")]
        public int ProvinciaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Localidad")]
        [Display(Name = @"Localidad")]

        public int LocalidadId { get; set; }
        public List<Models.Provincia> Provincias { get; set; }
        public List<Models.Localidad> Localidades { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Numero de documento")]

        public String NroDocumento { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Mail")]
        public string CorreoElectronico { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Nro. De telefono movil")]
        public string TelefonoMovil { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(255, ErrorMessage = "El campo {0} debe contener no más de {1} caracteres")]
        [Display(Name = @"Nro. De telefono fijo")]
        public string TelefonoFijo { get; set; }



        
    }
}
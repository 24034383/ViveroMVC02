using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViveroMVC02.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
    }
}
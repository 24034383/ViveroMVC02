using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViveroMVC02.ViewModels
{
    public class Listador<T> : PaginadorGenerico where T : class
    {
        public IEnumerable<T> Registros { get; set; }
    }
}
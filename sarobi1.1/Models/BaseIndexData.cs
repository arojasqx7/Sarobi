using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sarobi1._1.Models
{
    public class BaseIndexData
    {
        public IEnumerable<Base> Bases { get; set; }
        public IEnumerable<Empleado> Empleados { get; set; }
    }
}
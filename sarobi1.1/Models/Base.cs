using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sarobi1._1.Models
{
    public class Base
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Tracking> Tracking { get; set; }
    }
}
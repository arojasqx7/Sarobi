using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sarobi1._1.Models
{
    public class Empleado
    {
        public int ID { get; set; }
        public string PrimerNombre { get; set; }
        public string PrimerApellido { get; set; }

        public virtual ICollection<Base> Bases { get; set; }

    }
}
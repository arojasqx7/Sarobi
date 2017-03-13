using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sarobi1._1.Models
{
    public class Tracking
    {
        public int ID { get; set; }
        public int Horas { get; set; }
        public DateTime Fecha {get;set;}


        public virtual Empleado Empleado { get; set; }

    }
}
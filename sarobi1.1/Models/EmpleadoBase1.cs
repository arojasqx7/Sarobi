using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sarobi1._1.Models
{
    public class EmpleadoBase1
    {
        public int ID { get; set; }

        [ForeignKey("Empleado")]
        public int EmpleadoID { get; set; }

        [ForeignKey("Base")]
        public int BaseID { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Base Base { get; set; }
    }
}
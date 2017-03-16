using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sarobi1._1.Models
{
    public class Base
    {
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Encargado { get; set; }

        public string Telefono { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [ForeignKey("Empleado")]
        public int ID_Supervisor { get; set; }

        public virtual Empleado Supervisor { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Tracking> Tracking { get; set; }
    }
}
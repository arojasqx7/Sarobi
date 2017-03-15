using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sarobi1._1.Models
{
    public class Tracking
    {
        public int ID { get; set; }

        [Required]
        [Range(1,24,ErrorMessage ="Hora inválida.")]
        public int Horas { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha {get;set;}

        [ForeignKey("Base")]
        public int ID_Base { get; set; }

        [ForeignKey("Empleado")]
        public int ID_Empleado { get; set; }

        public virtual Base Base { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
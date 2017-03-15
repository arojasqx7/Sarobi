using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sarobi1._1.Models
{
    public class Empleado
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string PrimerNombre { get; set; }

        [StringLength(20)]
        public string PrimerApellido { get; set; }

        [StringLength(20)]
        public string SegundoApellido { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Sexo { get; set; }

        [Required]
        public string TipoIdentificacion { get; set; }

        [Required]
        public string NumeroIdentificacion { get; set; }

        public string Nacionalidad { get; set; }

        [Required]
        public string Telefono1 { get; set; }

        public string Telefono2 { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [Required]
        public string Puesto { get; set; }

        public string TipoEmpleado { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaContratacion { get; set; }

        [DataType(DataType.MultilineText)]
        public string Recomendaciones { get; set; }

        public string Foto { get; set; }

        public string AntecedentesPenales { get; set; }

        public string FullName
        {
            get
            {
                return PrimerNombre + "  " + PrimerApellido + "  " + SegundoApellido;
            }
        }

        public virtual ICollection<Base> Bases { get; set; }

    }
}
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

        [Required(ErrorMessage = "Campo Fecha de Nacimiento es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Campo Sexo es requerido")]
        public string Sexo { get; set; }

        [Required]
        public string TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "Campo Núm Id es requerido")]
        public string NumeroIdentificacion { get; set; }

        public string Nacionalidad { get; set; }

        [Required(ErrorMessage = "Campo Teléfono es requerido")]
        public string Telefono1 { get; set; }

        public string Telefono2 { get; set; }

        [Required(ErrorMessage = "Campo Dirección es requerido")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Campo Puesto es requerido")]
        public string Puesto { get; set; }

        public string TipoEmpleado { get; set; }

        [Required(ErrorMessage = "Campo Fecha de Contratación es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaContratacion { get; set; }

        [DataType(DataType.MultilineText)]
        public string Recomendaciones { get; set; }

        public string Foto { get; set; }

        public string AntecedentesPenales { get; set; }

        [Required(ErrorMessage = "Campo Username es requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Debe tener formato de correo")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Campo Contraseña es requerido")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

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
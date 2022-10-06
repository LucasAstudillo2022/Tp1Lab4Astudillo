using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Practica1.Models
{
    public class Autor
    {     
        public int ID { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string apellido { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string nombre { get; set; }

        [Display(Name = "Biografia")]
        [Required(ErrorMessage = "La Biografia es obligatorio")]
        public string biografia { get; set; }
        

        public string foto { get; set; }


    }
}

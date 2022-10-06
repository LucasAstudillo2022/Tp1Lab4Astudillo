using Practica1.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
[assembly: ComVisible(false)]
[assembly: Guid("ace40b2a-9071-41f2-a4ec-5e314181e5c9")]

namespace Practica1
{
    public class Libro
    {
        [Display(Name = "Id")]
      
        public int ID { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El Titulo es obligatorio")]
        public string titulo { get; set; }

        [Display(Name = "Resumen")]
        [Required(ErrorMessage = "El Resumemn es obligatorio")]
        public string resumen { get; set; }

        [Display(Name = "Fecha de Publicacion")]
        public DateTime fechapubli { get; set; }
        
        public string fotoportada { get; set; }

        public int generoId { get; set; }
        public int autorId { get; set; }

        public Genero genero { get; set; }
        public Autor autor { get; set; }
       
    }
}

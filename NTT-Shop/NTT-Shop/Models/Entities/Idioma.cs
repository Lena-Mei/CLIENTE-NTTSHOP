using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class Idioma
    {
        public int idIdioma { get; set; }
        [Display(Name ="Descripción")]
        [Required(ErrorMessage = "Se requiere introducir la descripción del idioma")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Descripción no puede contener números.")]
        public string descripcion { get; set; }
        [Display(Name = "ISO")]
        [Required(ErrorMessage = "Se requiere introducir la iso del idioma.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo ISO no puede contener números.")]
        public string iso { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class GesUsuario
    {
        public int IdUsuario { get; set; }

        public string Inicio { get; set; }

        public string Contrasenya { get; set; } 

        public string Nombre { get; set; } 

        public string Apellido1 { get; set; } 

        public string Apellido2 { get; set; }

        public string Email { get; set; }

        public string IsoIdioma { get; set; }
    }
}
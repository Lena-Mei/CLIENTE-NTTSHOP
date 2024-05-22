using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class DesProducto
    {
        public int idDesProducto { get; set; }
        public int idProducto { get; set; }
        public string isoIdioma { get; set; }
        public string nombre { get; set; }
        public string descripcion {  get; set; }

    }
}
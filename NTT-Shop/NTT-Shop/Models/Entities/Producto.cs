using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class Producto
    {
        public int idProducto { get; set; }
        public int stock {  get; set; }
        public bool habilitado { get; set; }
        public string imagen { get; set; }

        public List<DesProducto> descripcion {  get; set; }
        public List<ProductoRate> rate { get; set; }
    }
}
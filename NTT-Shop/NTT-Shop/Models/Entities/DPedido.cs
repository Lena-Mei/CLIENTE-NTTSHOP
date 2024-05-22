using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class DPedido
    {
        public int idPedido { get; set; }
        public int idProducto { get; set; }
        public decimal precio { get; set; } 
        public int unidades { get; set; }
        public string estado { get; set; }
        public List<DesProducto> descripcion { get; set; }
        public List<ProductoRate> rate { get; set; }
    }
}
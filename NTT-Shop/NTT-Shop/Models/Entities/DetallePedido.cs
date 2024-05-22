using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class DetallePedido
    {
        public int idPedido { get; set; }
        public int idProducto { get; set; }
        public decimal precio { get; set; } //Precio producto * Unidades
        public int unidades { get; set; }
        public Producto  producto { get; set; }
    }
}
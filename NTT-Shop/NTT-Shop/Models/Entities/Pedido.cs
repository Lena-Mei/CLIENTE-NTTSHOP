using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class Pedido
    {
        public int idPedido { get; set; }
        public DateTime fechaPedido { get; set; }
        public int idEstado { get; set; }
        public decimal totalPrecio { get; set; } // TotalPrecio 
        public int idUsuario { get; set; }
        public List<DetallePedido> detallePedido { get; set; }
    }
}
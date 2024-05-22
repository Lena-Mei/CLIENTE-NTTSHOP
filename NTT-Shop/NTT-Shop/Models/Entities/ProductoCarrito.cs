using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class ProductoCarrito
    {
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public int stock {  get; set; }
        public decimal total
        {
            get
            {
                return cantidad * producto[0].rate[0].precio;
            }
        }
        public List<Producto> producto { get; set; }
    }
}
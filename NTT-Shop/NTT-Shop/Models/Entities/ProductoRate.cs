﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class ProductoRate
    {
        public int idProducto {  get; set; }
        public int idRate { get; set; }
        public decimal precio { get; set; }
    }
}
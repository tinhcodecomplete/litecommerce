﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModel
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }

    }
}

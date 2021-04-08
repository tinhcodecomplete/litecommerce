using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModel
{
    public class OrderDetails
    {
        /// <summary>
        /// 
        /// </summary>
       public int OrderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
       public int ProductID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Discount { get; set; }

    }
}

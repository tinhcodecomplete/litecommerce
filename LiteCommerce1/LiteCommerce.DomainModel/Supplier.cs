using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModel
{
    /// <summary>
    /// thông tin về nhà cung cấp 
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String ContactName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String ContactTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Phone { get; set; 
        }/// <summary>
        /// 
        /// </summary>
        public String Fax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String HomePage { get; set; }
        
    }
}

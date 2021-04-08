using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class ProductAtrributeResult
    {
        public Product DataProducts;
        public List<ProductAttribute> DataProductAttributes;
        public int Category { get; set; }
        public int SupplierID { get; set; }
    }
}
using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class ProductPaginationResult : PaginationResult
    {
        public List<Product> Data;

        public int Category { get; set; }
        public int Supplier { get; set; }
    }
}
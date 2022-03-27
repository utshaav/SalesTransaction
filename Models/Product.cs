using System;
using System.Collections.Generic;

namespace SalesTransaction.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

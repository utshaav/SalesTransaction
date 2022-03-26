using System;
using System.Collections.Generic;

namespace SalesTransaction.Models
{
    public partial class Sale
    {
        public int Id { get; set; }
        public int? CostumerId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public bool? IsBilled { get; set; }
    }
}

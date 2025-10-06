using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrdered product { get; set; } = default!;
        public decimal price { get; set; }
        public int quantity { get; set; }

    }
}

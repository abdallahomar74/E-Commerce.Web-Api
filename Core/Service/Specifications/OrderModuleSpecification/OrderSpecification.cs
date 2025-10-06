using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.OrderModuleSpecification
{
    internal class OrderSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderSpecification(string Email) : base(O => O.UserEmail == Email)
        {
            AddInclude(O =>O.Items);
            AddInclude(O => O.DeliveryMethod);
            AddOrderByDescending(O => O.OrderDate);
        }
        public OrderSpecification(Guid id) : base(O => O.Id == id)
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DeliveryMethod);
        }
    }
}

using Demo.BLL.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specification
{
    public class OrderWithItemsAndOrderingSpecificaion : BaseSpecification<Order>
    {
        public OrderWithItemsAndOrderingSpecificaion(string email) : base(o=>o.BuyerEmail==email)
        {
            AddInclude(o => o.OrderTime);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescinding(o => o.OrderTime);
        }
        public OrderWithItemsAndOrderingSpecificaion(int id,string email) 
            : base(o => o.Id==id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderTime);
            AddInclude(o => o.DeliveryMethod);
           // AddOrderByDescinding(o => o.OrderTime);
        }
    }
}

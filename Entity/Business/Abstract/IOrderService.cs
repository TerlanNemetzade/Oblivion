using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll();
        void Add(Order order);
    }
}

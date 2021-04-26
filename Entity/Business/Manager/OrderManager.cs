using Entity.Business.Abstract;
using Entity.Domain.Abstract.IDal;
using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Business.Manager
{
    public class OrderManager : IOrderService
    {
        private  IOrderDal _shippingDal;
        public OrderManager(IOrderDal shippingDal)
        {
            _shippingDal = shippingDal;
        }

        public void Add(Order order)
        {
            _shippingDal.Add(order);
            
        }
        
        public List<Order> GetAll()
        {
            return _shippingDal.GetList();
        }
    }
}

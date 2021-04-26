using Entity.Business.Abstract;
using Entity.Domain.Abstract.IDal;
using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Business.Manager
{
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;
        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }
        public void Add(OrderDetail order)
        {
             _orderDetailDal.Add(order);
        }

        public List<OrderDetail> GetAll()
        {
            return _orderDetailDal.GetList();
        }
    }
}

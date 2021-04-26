using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblivion.Models
{
    public class OrderDetailModel
    {
        public int OrderID { get; set; }    
        public int ProductID { get; set; }  
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public List<OrderDetail> orderDetail { get; set; }
    }
}

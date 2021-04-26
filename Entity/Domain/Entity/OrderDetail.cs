using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Domain.Entity
{
    public class OrderDetail:IEntity
    {
        public int OrderID { get; set; }    
        public int ProductID { get; set; }  
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}

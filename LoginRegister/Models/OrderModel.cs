using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblivion.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public List<Order> Orders { get; set; }


    }
}

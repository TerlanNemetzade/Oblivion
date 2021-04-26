using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Domain.Entity
{
    public class Order : IEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public string UserID { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string Address { get; set; }
        //public string City { get; set; }

        //public string Phone { get; set; }

    }
}

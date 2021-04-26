using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Domain.Entity
{
    public class Product:IEntity
    {
      public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

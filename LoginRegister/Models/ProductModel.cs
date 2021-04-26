﻿using Entity.Domain.Entity;
using System.Collections.Generic;

namespace Oblivion.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public List<Product> Products { get; set; }

    }
}

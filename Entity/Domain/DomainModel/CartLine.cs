
using Entity.Domain.DomainModel;
using Entity.Domain.Entity;
using Entity.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DomainModel
{
    public class CartLine:IDomainModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

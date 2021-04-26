using Entity.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.DomainModel
{
    public class Cart:IDomainModel

    {
        public Cart()
        {
            CartLines = new List<CartLine>();
        }
       
        public List<CartLine> CartLines { get; set; }
        
    }
}

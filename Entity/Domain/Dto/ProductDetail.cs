
using Entity.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dto
{
    public class ProductDetail:IDto
    {
        public int ProductId { get; set; }  
        public string ProductName { get; set; }     
        public decimal UnitPrice { get; set; }  
        public string CategoryName { get; set; }
    }
}

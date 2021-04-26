using Entity.Domain.Entity;
using System.Collections.Generic;

namespace Oblivion.Models
{
    public class CategoryListViewModel
    {
        public List<Category> Categories { get; set; }
        public int CurrentCategory { get; set; }
    }
}

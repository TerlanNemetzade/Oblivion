using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}

using Entity.DataAccess.EfEntity;
using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Entity.Domain.Abstract.IDal
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
      
    }
}

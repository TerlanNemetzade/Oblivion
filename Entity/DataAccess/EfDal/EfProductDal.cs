using Entity.DataAccess.Context;
using Entity.DataAccess.EfEntity;
using Entity.Domain.Abstract.IDal;
using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Entity.DataAccess.EfDal
{
    public class EfProductDal :EfEntityRepositoryBase<Product, NorthwindContext>,IProductDal
    { 
    }
}

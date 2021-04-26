using Entity.DataAccess.EfEntity;
using Entity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Domain.Abstract.IDal
{
    public interface IOrderDal:IEntityRepository<Order>
    {
    }
}

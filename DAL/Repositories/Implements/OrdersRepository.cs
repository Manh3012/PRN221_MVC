using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infacstucture;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Implements
{
    public class OrdersRepository : FRMDbContextBase<Orders> , IOrdersRepository
    {
        private readonly FRMDbContext _dbContext;
        public OrdersRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }
    }
}

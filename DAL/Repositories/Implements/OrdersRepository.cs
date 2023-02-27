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

        public List<Orders> GetTotalOrderToday()
        {
            DateTime today = DateTime.Today;

            var orders = _dbContext.Orders.Where(o => o.CreatedDate == today);
            return orders.ToList();
        }

        public List<Orders> GetTotalOrderWeek()
        {
            DateTime today = DateTime.Today;

            var orders = _dbContext.Orders.Where(o => o.CreatedDate >= today.AddDays(-7) && o.CreatedDate <= today);
            return orders.ToList();
        }

        public List<Orders> GetTotalOrderLastThirtyDays()
        {
            DateTime today = DateTime.Today;

            var orders = _dbContext.Orders.Where(o => o.CreatedDate >= today.AddDays(-30) && o.CreatedDate <= today);
            return orders.ToList();
        }
    }
}

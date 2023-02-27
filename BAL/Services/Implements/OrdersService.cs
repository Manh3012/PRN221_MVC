using System;
using System.Linq;
using System.Text;
using DAL.Infacstucture;
using System.Threading.Tasks;
using DAL.Repositories.Interface;
using System.Collections.Generic;
using DAL.Entities;

namespace BAL.Services.Implements
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepository;
        private IUnitOfWork _unitOfWork;

        public OrdersService(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }

        public float GetTotalOrderToday()
        {
            var orders = _ordersRepository.GetTotalOrderToday();
            float total = 0;

            foreach (var item in orders)
            {
                total += item.Total;
            }

            return total;
        }

        public float GetTotalOrdersWeek()
        {
            var orders = _ordersRepository.GetTotalOrderWeek();

            float total = 0;

            foreach (var item in orders)
            {
                total += item.Total;
            }

            return total;
        }

        public float GetTotalOrderLastThirtyDays()
        {
            var orders = _ordersRepository.GetTotalOrderLastThirtyDays();

            float total = 0;

            foreach (var item in orders)
            {
                total += item.Total;
            }

            return total;
        }

        public int CountOrderLastThirtyDays()
        {
            var orders = _ordersRepository.GetTotalOrderLastThirtyDays();



            return orders.Count;
        }
    }
}

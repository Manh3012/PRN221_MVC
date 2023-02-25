using System;
using System.Linq;
using System.Text;
using DAL.Infacstucture;
using System.Threading.Tasks;
using DAL.Repositories.Interface;
using System.Collections.Generic;

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
    }
}

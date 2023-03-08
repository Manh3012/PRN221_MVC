using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infacstucture;
using System.Threading.Tasks;
using DAL.Repositories.Interface;
using System.Collections.Generic;

namespace BAL.Services.Implements
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
      public async Task<List<User>> GetAll()
        {
            List<User> users = null;
            return  users= await _userRepository.GetAll();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetUserbyUserEmail(email);
        }

        public async Task<User> GetById(string id)
        {
            return await _userRepository.GetUserbyID(id);
        }
    }
}

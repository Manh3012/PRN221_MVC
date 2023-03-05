using BAL.Services.Interface;
using DAL.Entities;
using DAL.Infacstucture;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class CartService : ICartService
    {
        private ICartRepository CartRepository;
        private IUnitOfWork UnitOfWork;

        public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            this.CartRepository = cartRepository;
            this.UnitOfWork = unitOfWork;
        }

        public void AddItem(User user, Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Cart cart)
        {
            CartRepository.DeleteItem(cart);
            UnitOfWork.Commit();
        }

        public Task<List<Cart>> GetCartList(User user)
        {
            return CartRepository.GetCartList(user);
        }

        public void UpdateQuantity(Cart cart, int quantity)
        {
            cart.Quantity = quantity;
            CartRepository.UpdateItem(cart);
            UnitOfWork.Commit();
        }

        public Cart GetCartById(string id)
        {
            return CartRepository.GetCartItem(new Guid(id)).Result;
        }
    }
}

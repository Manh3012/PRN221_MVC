using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interface
{
    public interface ICartService
    {
        Task<List<Cart>> GetCartList(User user);

        void AddItem(User user, Product product, int quantity);

        void UpdateQuantity(Cart cart, int quantity);

        void DeleteItem(Cart cart);

        Cart GetCartById(string id);

    }
}

using DAL.Entities;

namespace DAL.Repositories.Interface
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetCartList(User user);

        Task<Cart> GetCartItem(Guid guid);
        
        void AddItem(Cart cart);

        void DeleteItem(Cart cart);

        void UpdateItem(Cart cart);
    }
}

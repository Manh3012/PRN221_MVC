using DAL.Entities;
using DAL.Infacstucture;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Implements {
    public class TransactionRepository : FRMDbContextBase<Transaction>, ITransactionRepository {
        private readonly FRMDbContext _dbContext;
        public TransactionRepository(IDbFactory dbFactory) : base(dbFactory) {
            _dbContext = dbFactory.Init();
        }
    }
}

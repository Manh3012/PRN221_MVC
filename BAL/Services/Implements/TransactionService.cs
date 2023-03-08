using DAL.Infacstucture;
using DAL.Repositories.Interface;

namespace BAL.Services.Implements {
    public class TransactionService : ITransactionService {
        private ITransactionRepository _transactionRepository;
        private IUnitOfWork _unitOfWork;

        public TransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork) {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }
    }
}

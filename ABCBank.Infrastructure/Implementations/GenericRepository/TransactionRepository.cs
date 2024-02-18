using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.Categories;
using ABCBank.DTO.Transaction.Request;
using ABCBank.Infrastructure.Data;
using ABCBank.Response;

namespace ABCBank.Infrastructure.Implementations.GenericRepository
{
    public class TransactionRepository : GenericRepository<Transaction>,ITransactionRepository
    {
        private readonly ABCBankDbContext _context;

        public TransactionRepository(ABCBankDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<TransactionResponse<Transaction>> ApproveTransaction(Guid TransactionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaction>> GetAccountTransactions(Guid AccountId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TransactionResponse<Transaction>>> GetAllBankTransactions(Bank bank)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaction>> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaction>> GetUserTransactions(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResponse<Transaction>> RejectTransaction(Guid TransactionId)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResponse<Transaction>> SendMoney(CreateTransactionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResponse<Transaction>> UpdateTransaction(Guid TransactionId, UpdateTransactionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
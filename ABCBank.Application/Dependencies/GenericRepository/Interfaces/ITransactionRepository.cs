using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ABCBank.Domain.Categories;
using ABCBank.DTO.Transaction.Request;
using ABCBank.Response;


namespace ABCBank.Dependencies.GenericRepository.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<TransactionResponse<Transaction>> SendMoney(CreateTransactionDto dto);
        Task<List<Transaction>> GetAccountTransactions(Guid AccountId);
        Task<List<Transaction>> GetUserTransactions(Guid UserId);
        Task<List<TransactionResponse<Transaction>>> GetAllBankTransactions(Bank bank);
        Task<List<Transaction>> GetAllTransactions();
        Task<TransactionResponse<Transaction>> UpdateTransaction(
            Guid TransactionId,
            UpdateTransactionDto dto
        );
        Task<TransactionResponse<Transaction>> ApproveTransaction(Guid TransactionId);
        Task<TransactionResponse<Transaction>> RejectTransaction(Guid TransactionId);
    }
}

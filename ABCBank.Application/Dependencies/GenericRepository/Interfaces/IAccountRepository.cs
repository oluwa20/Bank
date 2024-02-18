using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Models;
using ABCBank.DTO.Account.Request;

namespace ABCBank.Dependencies.GenericRepository.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<IEnumerable<Account>> GetUserAccounts(Guid UserId);
        Task<string> GenerateRandomAccountNumber();
        Task<Account> UpdateAccountBalance(Guid AccountId, double Amount);
        Task<Account> GetAccountByAccountNumber(string accountNumber);
        Task<bool> DeleteUserAccounts(Guid UserId);
        Task<bool> AccountExists(Account account);
        Task<Account> UpdateAccount(Guid AccountId, Account acc);
    }
}

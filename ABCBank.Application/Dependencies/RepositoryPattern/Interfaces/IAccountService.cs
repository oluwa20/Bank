using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Models;
using ABCBank.DTO.Account.Request;


namespace ABCBank.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(CreateAccountDto dto);
        Task<List<Account>> GetUserAccounts(Guid UserId);
        Task<Account> UpdateAccountBalance(Guid AccountId,double Amount);
        Task<Account> GetAccountByAccountId(Guid Id);
        Task<Account> GetAccountByAccountNumber(GetAccountDto dto);
        Task<Account>  UpdateAccount(Guid Id,UpdateAccountDto dto);
        Task<Account>  DisableAccount(Guid Id);
        Task<bool> DeleteAccount(Guid Id);

        
    }
}
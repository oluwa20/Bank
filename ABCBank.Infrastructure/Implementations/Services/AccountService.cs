using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Application.Interfaces;
using ABCBank.Domain.Categories;
using ABCBank.Domain.Models;
using ABCBank.DTO.Account.Request;
using ABCBank.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ABCBank.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly ABCBankDbContext _context;
        private ICustomerService _customer;

        public AccountService(IMapper mapper, ABCBankDbContext context, ICustomerService customer)
        {
            _context = context;
            _mapper = mapper;
            _customer = customer;
        }

        public async Task<Account> CreateAccount(CreateAccountDto dto)
        {
            CustomerAccount customer = await _customer.GetCustomerAccountById(dto.CustomerId);
            if (customer == null)
            {
                return null;
            }
            Account account = _mapper.Map<Account>(dto);
            account.AccountId=Guid.NewGuid();
            // account.AccountUser = customer;
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetUserAccounts(Guid UserId)
        {
            return await _context.Accounts.Where(a => a.CustomerId == UserId).ToListAsync();
        }

        public async Task<Account> GetAccountByAccountId(Guid Id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x=>x.AccountId==Id);
        }

        public async Task<Account> GetAccountByAccountNumber(GetAccountDto dto)
        {
            return await _context.Accounts.Where(
                a => a.AccountNumber == dto.AccountNumber && a.BankName == dto.BankName
            ).Include(x=>x.Customer).FirstOrDefaultAsync();
        }

        public async Task<Account> UpdateAccount(Guid Id, UpdateAccountDto dto)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x=>x.AccountId==Id);
            if (account != null)
            {
                account.AccountBalance = dto.AccountBalance;
                account.AccountType = dto.AccountType;
                account.AccountStatus = dto.AccountStatus;
                // Update other properties if needed
                account.AccountUpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return account;
        }

        public async Task<Account> DisableAccount(Guid Id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x=>x.AccountId==Id);
            if (account != null)
            {
                account.AccountStatus = AccountStatus.InActive; // Update the status accordingly
                await _context.SaveChangesAsync();
            }
            return account;
        }

        public async Task<bool> DeleteAccount(Guid Id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x=>x.AccountId==Id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Account> UpdateAccountBalance(Guid AccountId, double Amount)
        {
           var account = await _context.Accounts.FirstOrDefaultAsync(x=>x.AccountId==AccountId);
            if (account != null)
            {
                account.AccountBalance =Amount;
                account.AccountUpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return account;
        }
    }
}

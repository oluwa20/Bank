using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.Categories;
using ABCBank.Domain.Models;
using ABCBank.DTO.Account.Request;
using ABCBank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABCBank.Infrastructure.Implementations.GenericRepository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly ABCBankDbContext _context;

        public AccountRepository(ABCBankDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> AccountExists(Account account)
        {
            var accountIdExists = await _context.Accounts.FindAsync(account.AccountId);
            if (accountIdExists != null)
            {
                throw new Exception("ACCOUNT WITH THIS ID ALREADY EXISTS");
            }

            var accountExists = await _context.Accounts.AnyAsync(
                x => x.AccountNumber == account.AccountNumber && x.BankName == account.BankName
            );
            if (accountExists)
            {
                throw new Exception("ACCOUNT ALREADY EXISTS IN THIS BANK WITH THIS ACCOUNT NUMBER");
            }
            return false;
        }

        public async Task<IEnumerable<Account>> GetUserAccounts(Guid CustomerId)
        {
            var user = await _context.Customers.FindAsync(CustomerId);
            if (user == null)
            {
                throw new Exception("USER NOT FOUND");
            }
            var userAccounts = await _context.Accounts.Where(x => x.CustomerId == CustomerId).ToListAsync();
            return userAccounts;
        }

        public async Task<bool> DeleteUserAccounts(Guid CustomerId)
        {
            var user = await _context.Customers.FindAsync(CustomerId);
            if (user == null)
            {
                throw new Exception("USER NOT FOUND");
            }
            var userAccounts = await _context.Accounts.Where(x => x.CustomerId == CustomerId).ToListAsync();
            if (userAccounts == null || userAccounts.Count() == 0)
            {
                return false;
            }
            foreach (var x in userAccounts)
            {
                _context.Accounts.Remove(x);
            }
            return true;
        }

        public async Task<Account> GetAccountByAccountNumber(string accountNumber)
        {
            var init = await _context.Accounts.Where(
                a => a.AccountNumber == accountNumber
            ).Include(x=>x.Customer).FirstOrDefaultAsync()
            ?? throw new Exception("ACCOUNT NOT FOUND");
            return init;
        }

        public async Task<Account> DisableAccount(Guid Id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountId == Id);
            if (account != null)
            {
                account.AccountStatus = AccountStatus.InActive; // Update the status accordingly
                await _context.SaveChangesAsync();
            }
            throw new Exception("ACCOUNT NOT FOUND");
        }

        public async Task<Account> UpdateAccount(Guid AccountId, Account acc)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(
                x => x.AccountId == AccountId
            );
            if (account != null)
            {
                _context.Accounts.Update(acc);
                await _context.SaveChangesAsync();
                return account;
            }
            throw new Exception("ACCOUNT NOT FOUND");
        }

        public async Task<Account> UpdateAccountBalance(Guid AccountId, double Amount)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(
                x => x.AccountId == AccountId
            );
            if (account != null)
            {
                account.AccountBalance = Amount;
                account.AccountUpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            throw new Exception("ACCOUNT NOT FOUND");
        }

        public async Task<string> GenerateRandomAccountNumber()
        {
            int accountNumberLength = 10;
            string prefix = "";

            // Generate a random number using the current time as the seed
            Random random = new Random((int)DateTime.Now.Ticks);

            // Generate a random number with the specified length
            string randomNumber = "";
            for (int i = 0; i < accountNumberLength - prefix.Length; i++)
            {
                randomNumber += random.Next(0, 10).ToString();
            }

            // Concatenate the prefix and the random number to form the final account number
            string accountNumber = prefix + randomNumber;
            var check = await _context.Accounts.AnyAsync(x => x.AccountNumber == accountNumber);
            if (!check)
            {
                return accountNumber;
            }

            return await GenerateRandomAccountNumber();
        }
    }
}

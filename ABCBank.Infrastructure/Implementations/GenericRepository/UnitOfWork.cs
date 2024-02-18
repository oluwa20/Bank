using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.RepositoryInterface;
using ABCBank.Infrastructure.Data;
using ABCBank.Infrastructure.RepositoryServices;

namespace ABCBank.Infrastructure.Implementations.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ABCBankDbContext _context;

        public UnitOfWork(ABCBankDbContext context)
        {
            _context = context;
            Accounts = new AccountRepository(_context);
            Transactions = new TransactionRepository(_context);
            Customers = new CustomerRepository(_context);
            UserTokens = new UserTokenRepository(_context);
        }

        public IAccountRepository Accounts { get; private set; }
        public IUserTokenRepository UserTokens { get; private set; }
        public ICustomerRepository Customers { get; private set; }

        public ITransactionRepository Transactions { get; set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

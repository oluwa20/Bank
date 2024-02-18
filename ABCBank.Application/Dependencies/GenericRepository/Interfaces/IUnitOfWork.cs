using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.RepositoryInterface;

namespace ABCBank.Dependencies.GenericRepository.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public IAccountRepository Accounts { get; }
        public ICustomerRepository Customers { get;  }
        public IUserTokenRepository UserTokens { get;  }
        public ITransactionRepository Transactions { get; }

        int Save();
        Task<int> SaveAsync();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.Models;
using ABCBank.Infrastructure.Data;


namespace ABCBank.Infrastructure.Implementations.GenericRepository
{
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(ABCBankDbContext context) : base(context)
        {
        }
    }
}
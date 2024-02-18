using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCBank.Infrastructure.Data
{
    public class ABCBankDbContext : DbContext
    {
        public ABCBankDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<CustomerAccount> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserToken> UserTokens{get;set;}
    }
}

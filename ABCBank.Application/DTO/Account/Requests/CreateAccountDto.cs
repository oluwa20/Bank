using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;

namespace ABCBank.DTO.Account.Request
{
    public class CreateAccountDto
    {
  
public Guid CustomerId { get; set; }

        public Bank BankName { get; set; }=Bank.ABCBank;
        public AccountType AccountType { get; set; }



    }
}
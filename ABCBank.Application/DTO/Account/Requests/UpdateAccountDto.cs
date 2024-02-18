using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;

namespace ABCBank.DTO.Account.Request
{
    public class UpdateAccountDto
    {
        public double AccountBalance { get; set; }=0.00;
        public AccountType AccountType { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Active;
        // public DateTime AccountRenewalDate { get; set; }   WILL LOVE TO ADD A FEATURE LIKE THIS LATER THO

        
    }
}
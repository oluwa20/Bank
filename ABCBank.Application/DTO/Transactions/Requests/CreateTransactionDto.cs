using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;
namespace ABCBank.DTO.Transaction.Request
{
    public class CreateTransactionDto
    {

        public Guid AccountId { get; set; }

        public String? AccountNumber { get; set; }
        public Bank BankName { get; set; }
        public double TransactionAmount { get; set; }
        
    }
}
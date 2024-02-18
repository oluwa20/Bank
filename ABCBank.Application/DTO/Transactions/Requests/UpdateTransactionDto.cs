using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;


namespace ABCBank.DTO.Transaction.Request
{
    public class UpdateTransactionDto
    {


        public Guid AccountId { get; set; }

       



        public double TransactionAmount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }=TransactionStatus.Pending;
        
    }
}
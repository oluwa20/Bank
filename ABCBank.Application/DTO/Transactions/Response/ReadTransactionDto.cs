using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ABCBank.Domain.Categories;


namespace ABCBank.DTO.Transaction.Request
{
    public class ReadTransactionDto
    {

        public Guid TransactionId { get; set; } 
        public Guid AccountId { get; set; }
        // public User TransactionAccount { get; set; }
        public DateTime TransactionTime { get; set; }
        public TransactionType TransactionType { get; set; }
        // public Guid TransactionSender { get; set; }
        // public User Sender {get;set;}
        public Guid TransactionReceiver { get; set; }
        // public User Receiver { get; set; }
        public double TransactionAmount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
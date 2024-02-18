using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;

namespace ABCBank.Domain.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public Account? Account { get; set; }

        // public Guid TransactionSender { get; set; }
        public DateTime TransactionTime { get; set; }
        public DateTime? TransactionUpdatedAt { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid ReceivingAccount { get; set; }
        public double TransactionAmount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }=TransactionStatus.Pending;
    }
}
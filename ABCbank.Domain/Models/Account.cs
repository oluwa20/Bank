using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace ABCBank.Domain.Models
{
//  [PrimaryKey("AccountNumber","BankName")]
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }=Guid.NewGuid();
        public string? AccountNumber { get; set; }
        public Bank BankName { get; set; }
        public AccountType AccountType { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        [JsonIgnore]
        public CustomerAccount? Customer { get; set; }

        public AccountStatus AccountStatus { get; set; }=AccountStatus.Active;
        public double AccountBalance { get; set; }
        public double AccountDailyLimit { get; set; }=5000.00;

        public DateTime AccountCreatedAt { get; set; }
        public DateTime AccountUpdatedAt { get; set; }
        public DateTime AccountExpiryDate { get; set; }
    }
}
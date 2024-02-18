using ABCBank.Domain.Categories;
using ABCBank.Domain.Models;


namespace ABCBank.DTO.Account.Response
{
    public class ReadAccountDto
    {
        
        public Guid AccountId { get; set; }
        public String? AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public Bank BankName { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerAccount? Customer{ get; set; }
        public AccountType AccountType { get; set; }
        public AccountStatus AccountStatus { get; set; } 
        // public List<User>? AccountBeneficiaries { get; set; }
        public DateTime AccountCreatedAt { get; set; } 


       
    
    }
}
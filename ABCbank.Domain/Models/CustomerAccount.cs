using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;

namespace ABCBank.Domain.Models
{
    public class CustomerAccount
    {
        [Key]
        public Guid CustomerId { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? FamilyName { get; set; }
        public string? Nationality { get; set; }
        public string? Gender { get; set; }
        public string? Residence { get; set; }
        public string? DateOfBirth { get; set; }  
        public string? Bvn { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Street { get; set; }
        public string? NearestLandmark { get; set; }
        public string? CountryOfBirth { get; set; }
        public string? OtherNationality { get; set; }
        public string? HomeAddress { get; set; }
        public string? HashPassword { get; set; }
        public bool? AccountIsActivated { get; set; }=true;
        public bool? AccountIsDisabled { get; set; }=true;
        public List<Account>? UserAccounts {get;set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }=DateTime.MinValue;
        
        [JsonIgnore]
        public string? Nuban { get; set; }
        // public AccountType? AccountType { get; set; } // THIS PROPERTY GOES ON THE ACCOUNT
        // public string? AccountName { get; set; } //THIS PROPERTY GOES ON THE ACCOUNT


    }

	

}

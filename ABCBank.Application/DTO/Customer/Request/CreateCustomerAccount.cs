using System.ComponentModel.DataAnnotations;
using ABCBank.Domain.Categories;
using ABCBank.Domain.Models;

namespace ABCBank.DTO.Customer.Request
{
    public class CreateCustomerAccount
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? FamilyName { get; set; }
        public string? Nationality { get; set; }
        public string? Gender { get; set; }
        public string? Residence { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Street { get; set; }
        public string? NearestLandmark { get; set; }
        public string? CountryOfBirth { get; set; }
        public string? OtherNationality { get; set; }
        public string? HomeAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}

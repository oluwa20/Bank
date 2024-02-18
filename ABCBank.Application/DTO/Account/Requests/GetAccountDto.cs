using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;


namespace ABCBank.DTO.Account.Request
{
    public class GetAccountDto
    {
        [Required]
        public String? AccountNumber { get; set; }
        public Bank BankName { get; set; }
    }
}
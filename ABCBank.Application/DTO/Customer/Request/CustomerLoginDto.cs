using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBank.Application.DTO.Customer.Request
{
    public class CustomerLoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}

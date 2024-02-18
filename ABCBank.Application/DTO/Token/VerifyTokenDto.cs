using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;


namespace ABCBank.DTO.Token
{
    public class VerifyTokenDto
    {
        public String? Token {get;set;}
        public TokenType TokenType {get;set;}
    }
}
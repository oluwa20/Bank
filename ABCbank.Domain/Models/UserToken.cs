using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;

namespace ABCBank.Domain.Models
{
    public class UserToken
    {
        [Key]
        public Guid TokenId { get; set; }
        public Guid UserId { get; set; }
        public string? Token { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime ExpiresAt { get; set; }=DateTime.Now.AddMinutes(10);
        public DateTime CreatedAt { get; set; }=DateTime.Now;
    }
}

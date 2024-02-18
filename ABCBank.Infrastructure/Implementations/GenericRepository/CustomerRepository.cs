using ABCBank.Application.DTO.Customer.Request;
using ABCBank.Domain.Models;
using ABCBank.Domain.RepositoryInterface;
using ABCBank.Infrastructure.Data;
using ABCBank.Infrastructure.Implementations.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCBank.Infrastructure.RepositoryServices
{
    public class CustomerRepository : GenericRepository<CustomerAccount>, ICustomerRepository
    {
        private readonly ABCBankDbContext _context;

        public CustomerRepository(ABCBankDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfFieldExists(CustomerAccount user)
        {
            return await _context.Customers.AnyAsync(
                x => x.Bvn == user.Bvn || x.Email == user.Email || x.PhoneNumber == user.PhoneNumber
            );
        }

        public async Task<CustomerAccount> Login(CustomerLoginDto dto)
        {
            try
            {
                var customer = await _context.Customers.Where(
                    x => x.Email == dto.Email
                ).Include(x=>x.UserAccounts).FirstOrDefaultAsync();
                if (customer == null)
                {
                    throw new NullReferenceException(
                        "CUSTOMER ACCOUNT NOT FOUND || INVALID EMAIL ADDRESS"
                    );
                }
                var passwordCheck = BCrypt.Net.BCrypt.Verify(dto.Password, customer.HashPassword);
                if (!passwordCheck)
                {
                    throw new UnauthorizedAccessException("LOGIN FAILED : INVALID PASSWORD");
                }
                return customer;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GenerateCustomerBVN()
        {
            int accountNumberLength = 10;
            string prefix = "220";

            // Generate a random number using the current time as the seed
            Random random = new Random((int)DateTime.Now.Ticks);

            // Generate a random number with the specified length
            string randomNumber = "";
            for (int i = 0; i < accountNumberLength - prefix.Length; i++)
            {
                randomNumber += random.Next(0, 10).ToString();
            }

            // Concatenate the prefix and the random number to form the final account number
            string generatedBVN = prefix + randomNumber;
            var check = await _context.Customers.AnyAsync(x => x.Bvn == generatedBVN);
            if (!check)
            {
                return generatedBVN;
            }

            return await GenerateCustomerBVN();
        }

        public async Task<bool> CheckIfFieldExists(String Field)
        {
            return await _context.Customers.AnyAsync(
                x => x.Bvn == Field || x.Email == Field || x.PhoneNumber == Field
            );
        }
    }
}

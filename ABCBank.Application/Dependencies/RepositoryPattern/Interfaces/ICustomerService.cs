using ABCBank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCBank.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerAccount> CreateBuyer(CustomerAccount customer);
        Task<List<CustomerAccount>> GetAllCustomers();
        Task<CustomerAccount> GetCustomerAccountById(Guid CustomerId);
        Task<CustomerAccount>GetCustomerByBVN(string BVN);
        Task<CustomerAccount>GetCustomerByAccountNumber(string AccountNumber);  
        Task UpdateCustomerAccount(CustomerAccount buyer);
        Task DeleteCustomerAccount(Guid Id);
    }
}

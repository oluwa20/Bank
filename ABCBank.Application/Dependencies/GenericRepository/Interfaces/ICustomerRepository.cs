using ABCBank.Application.DTO.Customer.Request;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCBank.Domain.RepositoryInterface
{
    public interface ICustomerRepository: IGenericRepository<CustomerAccount>
    {
        Task<bool> CheckIfFieldExists(CustomerAccount user);
        Task<bool> CheckIfFieldExists(String Field);
        Task<string> GenerateCustomerBVN();
        Task<CustomerAccount> Login(CustomerLoginDto dto);
    }
}

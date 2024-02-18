using ABCBank.Application.Interfaces;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.Models;
using ABCBank.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCBank.Application.Services
{
    public class CustomerService : ICustomerService
    {
        public IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerAccount> CreateBuyer(CustomerAccount customer)
        {
            var createAccount = await _unitOfWork.Customers.Add(customer);
            _unitOfWork.Save();
            return createAccount;
        }

        public async Task DeleteCustomerAccount(Guid Id)
        {
            var customer = await _unitOfWork.Customers.GetById(Id);
            if (customer != null)
            {
                await _unitOfWork.Customers.Delete(customer.CustomerId);
                _unitOfWork.Save();

            }
        }

        public async Task<List<CustomerAccount>> GetAllCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAll();
            return customers.ToList();
        }

        public async Task<CustomerAccount> GetCustomerAccountById(Guid CustomerId)
        {
            var customer = await _unitOfWork.Customers.GetById(CustomerId);
            return customer;
        }

        public async Task<CustomerAccount> GetCustomerByAccountNumber(string AccountNumber)
        {
            // var customer = await _unitOfWork.Customers.Get(AccountNumber);
            // return customer;
            throw new NotImplementedException();
        }

        public async Task<CustomerAccount> GetCustomerByBVN(string BVN)
        {
            // var customer = await _unitOfWork.Customers.GetByBVN(BVN);
            // return customer;
            throw new NotImplementedException();

        }

        public Task UpdateCustomerAccount(CustomerAccount buyer)
        {
            throw new NotImplementedException();
        }
    }
}

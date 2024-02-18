using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Models;
using ABCBank.DTO.Account.Request;
using ABCBank.DTO.Account.Response;
using ABCBank.DTO.Customer.Request;
using ABCBank.DTO.Transaction.Request;
using AutoMapper;

namespace ABCBank.Application.Dependencies.Helpers.Mapper
{
    public class ABCBankAutoMapperProfiles:Profile
    {
        public ABCBankAutoMapperProfiles()
        {
            CreateMap<CreateCustomerAccount,CustomerAccount>();










                 //ACCOUNT MAPPINGS
            CreateMap<CreateAccountDto,Account>();
            CreateMap<UpdateAccountDto,Account>();
            CreateMap<Account,ReadAccountDto>().ReverseMap();


            //TRANSACTIONS
            CreateMap<CreateTransactionDto,Transaction>();
            CreateMap<UpdateTransactionDto,Transaction>();
            CreateMap<Transaction,ReadTransactionDto>().ReverseMap();

        }
    }
}
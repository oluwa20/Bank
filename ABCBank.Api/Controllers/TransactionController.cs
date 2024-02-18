using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Application.Interfaces;
using ABCBank.Dependencies.Helpers;
using ABCBank.Domain.Categories;
using ABCBank.Domain.Models;
using ABCBank.DTO.Transaction.Request;
using ABCBank.Response;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABCBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly IMapper _mapper;
        private AutoDefaultResponse<Transaction> x = new();
        
        public TransactionController(IMapper mapper,ITransactionService service)
        {
            _service = service;
            _mapper=mapper;
            
        }
        
        [HttpPost("SendMoney")]
        public async Task<ActionResult<TransactionResponse<Transaction>>> SendMoney(CreateTransactionDto dto){
            AutoDefaultResponse<Transaction>x=new();
            try {
                // dto.BankName=Bank.ABCBank;
                var transaction=await _service.SendMoney(dto);
                if(transaction.Status==TransactionStatus.Failed||transaction.Status==TransactionStatus.Declined){
                    return BadRequest(transaction);
                }
                return Ok(transaction);
            }
            catch(Exception ex){
                return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet("AllTransactions")]
    
        public async Task<ActionResult<DefaultResponse<Transaction>>> AllTransactions()
        {
            List<Transaction> transactions = await _service.GetAllTransactions();

            var x = new AutoDefaultResponse<List<Transaction>>();
            var response = x.ConvertToGood("REQUEST SUCCESSFUL");
            response.Data = transactions;

            return Ok(response);
        }

        [HttpGet("GetTransactionsByUser/{userId}")]
        public async Task<ActionResult<List<Transaction>>> UserTransactions(Guid userId)
        {
            List<Transaction> transactions = await _service.GetUserTransactions(userId);

            var x = new AutoDefaultResponse<List<Transaction>>();
            var response = x.ConvertToGood("REQUEST SUCCESSFUL");
            response.Data = transactions;

            return Ok(response);
        }
        
        [HttpGet("GetTransactionsByAccount/{accountId}")]
        public async Task<ActionResult<List<Transaction>>> AccountTransactions(Guid accountId)
        {
            List<Transaction> transactions = await _service.GetAccountTransactions(accountId);

            var x = new AutoDefaultResponse<List<Transaction>>();
            var response = x.ConvertToGood("REQUEST SUCCESSFUL");
            response.Data = transactions;

            return Ok(response);
        }
    }
}
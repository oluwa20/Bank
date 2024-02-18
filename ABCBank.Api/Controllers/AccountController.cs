using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Api.Routes;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Dependencies.Helpers;
using ABCBank.Domain.Categories;
using ABCBank.Domain.Models;
using ABCBank.DTO.Account.Request;
using ABCBank.DTO.Account.Response;
using ABCBank.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ABCBank.Controllers
{
    [ApiController]
    [Route($"api/{Routes.Default}")]
    public class AccountController : ControllerBase
    {
        //HELPERS ARE HERE
        private ActionResult ValidateModelState()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return null;
        }

        private async Task<bool> ValidateUser(Guid CustomerId)
        {
            var user = await _unitOfWork.Customers.GetById(CustomerId);
            if (user == null)
            {
                throw new Exception("INVALID CUSTOMER ID");
            }
            return true;
        }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _unit;
        private AutoDefaultResponse<ReadAccountDto> x = new();

        //HELPERS END HERE

        public AccountController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unit = _unitOfWork.Accounts;
        }

        [HttpPost("{CustomerId}")]
        public async Task<ActionResult<DefaultResponse<ReadAccountDto>>> CreateAccount(
            [FromBody] CreateAccountDto dto,
            [FromRoute] Guid CustomerId
        )
        {
            ValidateModelState();

            try
            {
                var validUser = await ValidateUser(CustomerId);

                if (!validUser)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO CREATE ACCOUNT : INVALID USER"));
                }
                var account = _mapper.Map<Account>(dto);
                account.CustomerId = CustomerId;
                account.AccountNumber = await _unit.GenerateRandomAccountNumber();
                var accountExists = await _unit.AccountExists(account);
                if (accountExists)
                {
                    return BadRequest(x.ConvertToBad("ACCOUNT ALREADY EXISTS"));
                }
                var result = await _unit.Add(account);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO CREATE A NEW ACCOUNT"));
                }
                var accountDto = _mapper.Map<ReadAccountDto>(account);
                var response = x.ConvertToGood("ACCOUNT CREATED SUCCESSFULLY", accountDto);
                await _unitOfWork.SaveAsync();
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet]
        public async Task<
            ActionResult<DefaultResponse<IEnumerable<ReadAccountDto>>>
        > GetAllAccounts()
        {
            AutoDefaultResponse<List<ReadAccountDto>> x =
                new AutoDefaultResponse<List<ReadAccountDto>>();

            try
            {
                var result = await _unit.GetAll();
                if (result == null || result.Count() == 0)
                {
                    return NotFound(x.ConvertToBad("NO ACCOUNTS FOUND"));
                }
                var accountDto = _mapper.Map<List<ReadAccountDto>>(result);
                var response = x.ConvertToGood("ACCOUNTS FETCHED SUCCESSFULLY", accountDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet]
        public async Task<
            ActionResult<DefaultResponse<IEnumerable<ReadAccountDto>>>
        > GetActiveAccounts()
        {
            AutoDefaultResponse<List<ReadAccountDto>> x =
                new AutoDefaultResponse<List<ReadAccountDto>>();

            try
            {
                var result = await _unit.GetAll();
                if (result == null || result.Count() == 0)
                {
                    return NotFound(x.ConvertToBad("NO ACCOUNT FOUND"));
                }
                var accountDto = _mapper.Map<List<ReadAccountDto>>(
                    result.Where(x => x.AccountStatus == AccountStatus.Active)
                );
                if (accountDto == null || accountDto.Count() == 0)
                {
                    return NotFound(x.ConvertToBad("NO ACTIVE ACCOUNT FOUND"));
                }
                var response = x.ConvertToGood("ACCOUNTS FETCHED SUCCESSFULLY", accountDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet("{AccountId}")]
        public async Task<ActionResult<DefaultResponse<ReadAccountDto>>> GetAccountById(
            [FromRoute] Guid AccountId
        )
        {
            try
            {
                var result = await _unit.GetById(AccountId);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("NO ACCOUNT FOUND: INVALID ACCOUNT ID"));
                }
                result.Customer=await _unitOfWork.Customers.GetById(result.CustomerId);
                var accountDto = _mapper.Map<ReadAccountDto>(result);
                var response = x.ConvertToGood("ACCOUNT FETCHED SUCCESSFULLY", accountDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet("{AccountNumber}")]
        public async Task<ActionResult<DefaultResponse<ReadAccountDto>>> GetAccountByAccountNumber(
            [FromRoute] String AccountNumber
        )
        {
            try
            {
                var result = await _unit.GetAccountByAccountNumber(AccountNumber);
                if (result == null)
                {
                    return BadRequest(
                        x.ConvertToBad("NO ACCOUNT FOUND: INVALID ACCOUNT NUMBER OR BANK")
                    );
                }
                var accountDto = _mapper.Map<ReadAccountDto>(result);
                var response = x.ConvertToGood("ACCOUNT FETCHED SUCCESSFULLY", accountDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet("{UserId}")]
        public async Task<
            ActionResult<DefaultResponse<IEnumerable<ReadAccountDto>>>
        > GetUserAccounts([FromRoute] Guid UserId)
        {
            AutoDefaultResponse<IEnumerable<ReadAccountDto>> x = new();
            try
            {
                var result = await _unit.GetUserAccounts(UserId);
                if (result == null || result.Count() == 0)
                {
                    return Ok(x.ConvertToGood("USER DOESNT HAVE ANY ACCOUNTS CURRENTLY"));
                }
                var accountDto = _mapper.Map<List<ReadAccountDto>>(result);
                var response = x.ConvertToGood("USER ACCOUNT FETCHED SUCCESSFULLY", accountDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpDelete("{AccountId}")]
        public async Task<ActionResult<DefaultResponse<ReadAccountDto>>> DeleteAccount(
            [FromRoute] Guid AccountId
        )
        {
            try
            {
                var result = await _unit.Delete(AccountId);
                if (!result)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO DELETE ACCOUNT"));
                }
                var response = x.ConvertToGood("ACCOUNT DELETED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();

                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpPut("{AccountId}")]
        public async Task<ActionResult<DefaultResponse<ReadAccountDto>>> UpdateAccount(
            [FromRoute] Guid AccountId,
            [FromBody] UpdateAccountDto dto
        )
        {
            ValidateModelState();

            try
            {
                var account = await _unit.GetById(AccountId);
                if (account == null)
                {
                    return BadRequest(x.ConvertToBad("INVALID ACCOUNT ID"));
                }

                var data = _mapper.Map(dto, account);
                data.AccountId = AccountId;
                account.AccountUpdatedAt = DateTime.Now;
                var result = await _unit.UpdateAccount(AccountId, data);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO UPDATE ACCOUNT"));
                }
                var response = x.ConvertToGood("ACCOUNT UPDATED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpPatch("{AccountId}")]
        public async Task<ActionResult<DefaultResponse<ReadAccountDto>>> DisableAccount(
            [FromRoute] Guid AccountId
        )
        {
            try
            {
                var account = await _unit.GetById(AccountId);
                if (account == null)
                {
                    return BadRequest(x.ConvertToBad("INVALID ACCOUNT ID"));
                }

                account.AccountStatus = AccountStatus.InActive;
                account.AccountUpdatedAt = DateTime.Now;
                var result = await _unit.UpdateAccount(AccountId, account);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO DISABLE ACCOUNT"));
                }
                var response = x.ConvertToGood("ACCOUNT DISABLED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpPatch("{AccountId}")]
        public async Task<ActionResult<DefaultResponse<ReadAccountDto>>> EnableAccount(
            [FromRoute] Guid AccountId
        )
        {
            try
            {
                var account = await _unit.GetById(AccountId);
                if (account == null)
                {
                    return BadRequest(x.ConvertToBad("INVALID ACCOUNT ID"));
                }

                account.AccountStatus = AccountStatus.Active;
                account.AccountUpdatedAt = DateTime.Now;
                var result = await _unit.UpdateAccount(AccountId, account);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO ENABLE ACCOUNT"));
                }
                var response = x.ConvertToGood("ACCOUNT ENABLED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        ///DO NOT CROSS
    }
}

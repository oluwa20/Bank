using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.RepositoryInterface;
using ABCBank.Application.Interfaces;
using ABCBank.Dependencies.Helpers;
using ABCBank.DTO.Customer.Request;
using ABCBank.Domain.Models;
using ABCBank.Response;
using ABCBank.Api.Routes;
using ABCBank.Domain.Categories;
using System.ComponentModel.DataAnnotations;
using ABCBank.Application.DTO.Customer.Request;

namespace ABCBank.Controllers
{
    [ApiController]
    [Route($"api/{Routes.Default}")]
    public class CustomerController : ControllerBase
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

        private async Task<ActionResult> ValidateUserDetails(CreateCustomerAccount dto)
        {
            var uniqueCheck = await _unit.CheckIfFieldExists(_mapper.Map<CustomerAccount>(dto));
            var phoneCheck = await _unit.CheckIfFieldExists(dto.PhoneNumber!);
            var emailCheck = await _unit.CheckIfFieldExists(dto.Email!);
            if (phoneCheck)
            {
                return BadRequest(x.ConvertToBad("PHONE NUMBER IS BEING USED BY ANOTHER CUSTOMER"));
            }
            if (emailCheck)
            {
                return BadRequest(x.ConvertToBad("EMAIL IS BEING USED BY ANOTHER CUSTOMER"));
            }
            if (uniqueCheck)
            {
                return BadRequest(
                    x.ConvertToBad("USERNAME OR EMAIL OR PHONE HAS ALREADY BEEN TAKEN")
                );
            }
            return Ok();
        }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _unit;
        private readonly ICustomerService _service;
        private AutoDefaultResponse<CustomerAccount> x = new();
        private EmailSender _mail;

        //HELPERS END HERE
        private readonly IConfiguration _configuration;

        public CustomerController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            ICustomerService service
        )
        {
            _service = service;
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unit = _unitOfWork.Customers;
            _mail = new EmailSender(_unitOfWork);
        }

        [HttpPost]
        // [AllowAnonymous]
        public async Task<ActionResult<DefaultResponse<CustomerAccount>>> CreateUser(
            [FromBody] CreateCustomerAccount dto
        )
        {
            ValidateModelState();

            var user = _mapper.Map<CustomerAccount>(dto);
            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            try
            {
                user.Bvn = await _unit.GenerateCustomerBVN();
                await ValidateUserDetails(dto);
                var result = await _unit.Add(user);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO CREATE A  NEW CUSTOMER"));
                }
                var userDto = _mapper.Map<CustomerAccount>(result);
                var response = x.ConvertToGood("CUSTOMER ACCOUNT CREATED SUCCESSFULLY", userDto);
             //   bool MailSent = await _mail.SendUserMail(result, TokenType.AccountActivation);
                await _unitOfWork.SaveAsync();
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet]
        // [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DefaultResponse<IEnumerable<CustomerAccount>>>> GetAllUsers()
        {
            AutoDefaultResponse<List<CustomerAccount>> x =
                new AutoDefaultResponse<List<CustomerAccount>>();

            try
            {
                var result = await _unit.GetAll();
                if (result == null || result.Count() == 0)
                {
                    return BadRequest(x.ConvertToBad("NO CUSTOMER FOUND"));
                }
                var userDto = _mapper.Map<List<CustomerAccount>>(result);
                var response = x.ConvertToGood("USERS FETCHED SUCCESSFULLY", userDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet]
        public async Task<
            ActionResult<DefaultResponse<IEnumerable<CustomerAccount>>>
        > GetAllActiveUsers()
        {
            AutoDefaultResponse<List<CustomerAccount>> x =
                new AutoDefaultResponse<List<CustomerAccount>>();

            try
            {
                var result = await _unit.GetAll();
                // result = result.Where(
                //     x => x.AccountIsDisabled == false && x.AccountIsActivated == true
                // );
                if (result == null || result.Count() == 0)
                {
                    return BadRequest(x.ConvertToBad("NO CUSTOMER FOUND"));
                }
                var userDto = _mapper.Map<List<CustomerAccount>>(result);
                var response = x.ConvertToGood("USERS FETCHED SUCCESSFULLY", userDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpGet(Routes.Parameter)]
        public async Task<ActionResult<DefaultResponse<CustomerAccount>>> GetUserById(
            [FromRoute] Guid Id
        )
        {
            try
            {
                Guid UserId = Id;
                var result = await _unit.GetById(UserId);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO FETCH CUSTOMER"));
                }
                var userDto = _mapper.Map<CustomerAccount>(result);
                var response = x.ConvertToGood("CUSTOMER FETCHED SUCCESSFULLY", userDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<DefaultResponse<String>>> ForgotPassword(
            [FromBody] [EmailAddress] String? EmailAddress
        )
        {
            ValidateModelState();
            try
            {
                var user = await _unit.CheckIfFieldExists(EmailAddress!);
                if (user == null)
                {
                    return NotFound(x.ConvertToBad("ACTION FAILED : WRONG EMAIL ADDRESS"));
                }

                // bool MailSent = await _mail.PasswordResetMail(user, TokenType.PasswordReset);
                await _unitOfWork.SaveAsync();
                return Ok(x.ConvertToGood("PASSWORD RESET TOKEN SENT SUCCESSFULLY"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<DefaultResponse<CustomerAccount>>> Login(
            CustomerLoginDto dto
        )
        {
            try
            {
                var result = await _unit.Login(dto);
                if (result == null)
                {
                    return Unauthorized(x.ConvertToBad("LOGIN FAILED : UNABLE TO LOGIN"));
                }
                // var userDto = _mapper.Map<CustomerAccount>(result);
                // string token = OTP.(result);
                // var response = x.ConvertToGood("LOGIN SUCCESSFULL! " + $"TOKEN : {token}", userDto);
                var response = x.ConvertToGood("LOGIN SUCCESSFULL!", result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        ////////////THIS IS THE PLACE , PPLS COME HERE AND MAKE CHANGES  LATER
        // [HttpPost("VerifyToken/{UserId}")]
        // [AllowAnonymous]
        // public async Task<ActionResult<DefaultResponse<CustomerAccount>>> VerifyToken(
        //     [FromRoute] Guid UserId,
        //     [FromBody] VerifyTokenDto tk
        // )
        // {
        //     // try
        //     // {
        //     //     var result = await _unit.VerifyToken(UserId, tk.Token!, tk.TokenType);
        //     //     if (result != true)
        //     //     {
        //     //         return Unauthorized(x.ConvertToBad("TOKEN VERIFICATION FAILED"));
        //     //     }
        //     //     var userAccountActivateRequest = await ActivateUserAccount(UserId);
        //     //     var response = x.ConvertToGood("TOKEN VERIFICAITION SUCCESSFULL! ");
        //     //     return Ok(response);
        //     // }
        //     // catch (Exception ex)
        //     // {
        //     //     return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
        //     // }
        // }

        [HttpDelete(Routes.Parameter)]
        public async Task<ActionResult<DefaultResponse<CustomerAccount>>> DeleteUser(
            [FromRoute] Guid Id
        )
        {
            try
            {
                Guid UserId = Id;
                var result = await _unit.Delete(UserId);
                var deleteUserAccountsRequest = await _unitOfWork.Accounts.DeleteUserAccounts(
                    UserId
                );
                if (!result)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO DELETE CUSTOMER"));
                }
                if (!deleteUserAccountsRequest)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO DELETE CUSTOMER ACCOUNTS"));
                }
                var response = x.ConvertToGood("CUSTOMER DELETED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DefaultResponse<CustomerAccount>>> UpdateUser(
            [FromRoute] Guid Id,
            [FromBody] CreateCustomerAccount dto
        )
        {
            ValidateModelState();

            try
            {
                Guid UserId = Id;
                var data = _mapper.Map<CustomerAccount>(dto);
                data.UpdatedAt = DateTime.Now;
                var result = await _unit.Update(UserId, data);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO UPDATE CUSTOMER"));
                }
                var response = x.ConvertToGood("CUSTOMER UPDATED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();
                return StatusCode(204, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        // [HttpPut("ChangePassword/{Id}")]
        // [Authorize(Roles="CUSTOMER")]
        // public async Task<ActionResult<DefaultResponse<CustomerAccount>>> ChangePassword(
        //     [FromRoute] Guid Id,
        //     [FromBody]ChangePasswordDto dto
        // )
        // {
        //     ValidateModelState();

        //     try
        //     {
        //         Guid UserId = Id;

        //         var data =await _unit.GetById(UserId);
        //         if(data==null){
        //             throw new NullReferenceException("CUSTOMER NOT FOUND : INVALID CUSTOMER ID");
        //         }
        //         data.Password=BCrypt.Net.BCrypt.HashPassword(dto.ConfirmPassword);
        //         data.UpdtaedAt = DateTime.Now;
        //         var result = await _unit.Update(UserId, data);
        //         if (result == null)
        //         {
        //             return BadRequest(x.ConvertToBad("UNABLE TO UPDATE CUSTOMER PASSWORD"));
        //         }
        //         var response = x.ConvertToGood("CUSTOMER PASSWORD UPDATED SUCCESSFULLY");
        //         await _unitOfWork.SaveAsync();
        //         return StatusCode(204, response);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
        //     }
        // }

        [HttpPatch("{UserId}")]
        public async Task<ActionResult<DefaultResponse<CustomerAccount>>> ActivateUserAccount(
            [FromRoute] Guid UserId
        )
        {
            ValidateModelState();

            try
            {
                var user = await _unit.GetById(UserId);
                if (user == null)
                {
                    return BadRequest(x.ConvertToBad("INVALID CUSTOMER ID : CUSTOMER NOT FOUND"));
                }
                user.AccountIsActivated = true;
                var result = await _unit.Update(UserId, user);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO ACTIVATE CUSTOMER ACCOUNT"));
                }
                var response = x.ConvertToGood("CUSTOMER ACCOUNT ACTIVATED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpPatch("{UserId}")]
        public async Task<ActionResult<DefaultResponse<CustomerAccount>>> DisableUserAccount(
            [FromRoute] Guid UserId
        )
        {
            ValidateModelState();

            try
            {
                var user = await _unit.GetById(UserId);
                if (user == null)
                {
                    return BadRequest(x.ConvertToBad("INVALID CUSTOMER ID : CUSTOMER NOT FOUND"));
                }
                user.AccountIsDisabled = true;
                var result = await _unit.Update(UserId, user);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO DISABLE CUSTOMER ACCOUNT"));
                }
                var response = x.ConvertToGood("CUSTOMER ACCOUNT DISABLED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        [HttpPatch("{UserId}")]
        public async Task<ActionResult<DefaultResponse<CustomerAccount>>> EnableUserAccount(
            [FromRoute] Guid UserId
        )
        {
            ValidateModelState();

            try
            {
                var user = await _unit.GetById(UserId);
                if (user == null)
                {
                    return BadRequest(x.ConvertToBad("INVALID CUSTOMER ID : CUSTOMER NOT FOUND"));
                }
                user.AccountIsDisabled = false;
                var result = await _unit.Update(UserId, user);
                if (result == null)
                {
                    return BadRequest(x.ConvertToBad("UNABLE TO ENABLE CUSTOMER ACCOUNT"));
                }
                var response = x.ConvertToGood("CUSTOMER ACCOUNT ENABLED SUCCESSFULLY");
                await _unitOfWork.SaveAsync();
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
        }

        ///DO NOT CROSS





        //JWT TOKEN CREATOR
        // private string CreateToken(CustomerAccount user)
        // {
        //     List<Claim> claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, user.FirstName!),
        //         new Claim(ClaimTypes.Email, user.Email!)
        //     };
        //     Claim role;
        //     if (user.Email == "dabirideji@gmail.com")
        //     {
        //         role = new Claim(ClaimTypes.Role, "ADMIN");
        //     }
        //     else
        //     {
        //         role = new Claim(ClaimTypes.Role, "CUSTOMER");
        //     }
        //     claims.Add(role);
        //     var key = new SymmetricSecurityKey(
        //         Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!)
        //     );

        //     var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //     var token = new JwtSecurityToken(
        //         expires: DateTime.Now.AddMinutes(30),
        //         signingCredentials: credentials,
        //         claims: claims
        //     );
        //     var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //     return jwt;
        // }
    }
}

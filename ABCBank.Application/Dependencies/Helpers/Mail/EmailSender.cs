using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.Categories;
using ABCBank.Domain.Models;


namespace ABCBank.Dependencies.Helpers
{
    public class EmailSender
    {
        private readonly Mailer _mail;
        private readonly IUnitOfWork _unitOfWork;
        public EmailSender(IUnitOfWork unit)
        {
            _unitOfWork=unit;
            _mail=new Mailer();
        }
        public async Task<bool> SendUserMail(CustomerAccount customer,TokenType type){

               OTP otp=new  OTP();
            string code=otp.GenerateOtp();
                UserToken tk = new UserToken
                {
                    UserId = customer.CustomerId,
                    Token = code,
                    TokenType = type
                };
                
                await _unitOfWork.UserTokens.Add(tk);
            // return _mail.SendMail(customer.Email!,$"YOUR TOKEN IS {tk.Token!}",type.ToString());
            return _mail.SendMail(customer.Email!,MailScenerios.AccountVerificationWithOtp(customer,tk),type.ToString());
        }
        public async Task<bool> PasswordResetMail(CustomerAccount customer,TokenType type){

               OTP otp=new  OTP();
            string code=otp.GenerateOtp();
                UserToken tk = new UserToken
                {
                    UserId = customer.CustomerId,
                    Token = code,
                    TokenType = type
                };
                
                await _unitOfWork.UserTokens.Add(tk);
            // return _mail.SendMail(customer.Email!,$"YOUR TOKEN IS {tk.Token!}",type.ToString());
            return _mail.SendMail(customer.Email!,MailScenerios.PasswordReset(customer,tk),type.ToString());
        }
    }
}
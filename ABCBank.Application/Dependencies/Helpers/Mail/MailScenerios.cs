using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Models;

namespace ABCBank.Dependencies.Helpers
{
    public class MailScenerios
    {
        public static string AccountVerificationWithOtp(CustomerAccount customer, UserToken tk)
        {
            return $@"
Hi {customer.FirstName},

Welcome to ABC Banking! To activate your account, please use the code below:

Token : {tk.Token}

If you did not sign up for an account with us, you can safely ignore this email.

Best,
ABC Bank
";
        }

        public static string PasswordReset(CustomerAccount customer, UserToken tk)
        {
            string emailBody =
                $@"
Hello {customer.FirstName},

We received a request to reset the password for your ABCBank account. To proceed with the password reset, click the link below:

Password Reset Token: {tk.Token}

If you didn't request a password reset or if you didn't initiate this request, please ignore this email.

Regards,
ABCBank Team
";
            return emailBody;
        }

        public static string EmailVerification(CustomerAccount customer, UserToken tk)
        {
            string emailBody =
                $@"
Dear {customer.FirstName},

Thank you for registering with ABCBank. To complete your registration, please click the link below to verify your email address:

Verification Link: {tk.Token}

If you did not create an account with us or did not initiate this request, please ignore this email.

Best regards,
ABCBank Team
";
            return emailBody;
        }
    }
}

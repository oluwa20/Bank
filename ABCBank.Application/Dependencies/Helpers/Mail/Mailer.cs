using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace ABCBank.Dependencies.Helpers
{
    public class Mailer
    {
            public bool SendMail(
            [EmailAddress] string mail,
            [Required] string MailMessgae,
            [Required] [MaxLength(20, ErrorMessage = "MAIL SUBJECT TOO LONG")] String MailSubject
        )
        {
            try
            {
                MailMessage m = new MailMessage();
                m.To.Add(mail);
                m.From = new MailAddress("dabirideji@gmail.com");
                m.Body = MailMessgae;
                m.Subject ="TOPGEMI : "+MailSubject;
                m.ReplyToList.Add("noreply@topgemi.com");

                SmtpClient sm = new SmtpClient("smtp.gmail.com")
                {
                    EnableSsl = true,
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("dabirideji@gmail.com", "bloncjzcgnwsnprr")
                };
                sm.Send(m);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }






    }
}
namespace ABCBank.Dependencies.Helpers
{
    public class OTP
    {
        public string GenerateOtp()
        {
            Random random = new Random();
            int code = random.Next(1000, 10000);
            return code.ToString("D4");
        }

    
    }
}

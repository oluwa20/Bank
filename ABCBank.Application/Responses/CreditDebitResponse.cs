using Newtonsoft.Json;

namespace ABCBank.Response
{
    public class CreditDebitResponse
    {
        public string? Currency { get; set; }
        public string? CrAccountNumber { get; set; }
        public string? CrBranchCode { get; set; }
        public string? DrAccountNumber { get; set; }
        public string? DrBranchCode { get; set; }
        public string? Amount { get; set; }
        public string? DrBalance { get; set; }
        public string? StatusCode { get; set; }
       
        public string? TransactionMessage { get; set; }
       
        public string? TransactionReference { get; set; }
      
        public string? TransactionId { get; set; }
        [JsonIgnore]
        public string? ReversalResponse { get; set; }
        [JsonIgnore]
        public bool IsReversal { get; set; }
        [JsonIgnore]
        public bool IsReversalSuccessful { get; set; }
    }
}

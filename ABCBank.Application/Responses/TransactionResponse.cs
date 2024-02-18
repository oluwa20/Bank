using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Domain.Categories;

namespace ABCBank.Response
{
    public class TransactionResponse<T>
    {
        public TransactionStatus Status { get; set; }
        public string? ResponseCode { get; set; }
        public String? StatusCode { get; set; }
        public string? ResponseMessage { get; set; }
        public T? Data { get; set; }
    }



    public class AutoTransactionResponse<T>
    {
            public TransactionResponse<T> Refunded(String message)
        {
            return new TransactionResponse<T>()
            {
                Status = TransactionStatus.Refunded,
                StatusCode="200",
                ResponseCode = "00",
                ResponseMessage = message,
            };
        }
        public TransactionResponse<T> Approved(String message)
        {
            return new TransactionResponse<T>()
            {
                Status = TransactionStatus.Successful,
                StatusCode="201",
                ResponseCode = "00",
                ResponseMessage = message
            };
        }
        public TransactionResponse<T> Pending(String message)
        {
            return new TransactionResponse<T>()
            {
                Status = TransactionStatus.Pending,
                StatusCode="200",
                ResponseCode = "00",
                ResponseMessage = message
            };
        }
        public TransactionResponse<T> Declined(String message)
        {
            return new TransactionResponse<T>()
            {
                Status = TransactionStatus.Declined,
                StatusCode="400",
                ResponseCode = "99",
                ResponseMessage = message
            };
        }
        public TransactionResponse<T> Failed(String message)
        {
            return new TransactionResponse<T>()
            {
                Status = TransactionStatus.Failed,
                StatusCode="500",
                ResponseCode = "99",
                ResponseMessage = message
            };
        }
        public TransactionResponse<T> Success(String message)
        {
            return new TransactionResponse<T>()
            {
                Status = TransactionStatus.Successful,
                StatusCode="200",
                ResponseCode = "00",
                ResponseMessage = message
            };
        }
    
    }
}

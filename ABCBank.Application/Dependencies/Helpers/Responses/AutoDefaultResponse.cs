using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Response;

namespace ABCBank.Dependencies.Helpers
{
    public class AutoDefaultResponse<T> where T:class
    {
        public DefaultResponse<T> ConvertToGood(String message,T? Data=null){
            return new DefaultResponse<T>(){
                Status=true,
                ResponseCode="00",
                ResponseMessage=message,
                Data=Data
            };
        }
        public DefaultResponse<T> ConvertToBad(String message){
            return new DefaultResponse<T>(){
                Status=false,
                ResponseCode="99",
                ResponseMessage=message,
            };
        }
    }
}
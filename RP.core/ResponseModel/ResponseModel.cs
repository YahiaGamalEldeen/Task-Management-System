using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.core.ResponseModel
{
    public class ResponseModel<T> 
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }

        public ResponseModel(bool isSuccess, T data, string message = "")
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public static ResponseModel<T> Success(T data, string message = "")
        {
                return new ResponseModel<T>(true, data, message);
        }

        

        // Static method to return an error response
        public static ResponseModel<T> Error(string message)
        {
            return new ResponseModel<T>(false, default , message);
        }


    }
}

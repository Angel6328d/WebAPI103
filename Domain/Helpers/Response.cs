using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public class Response<T>
    {
        public Response()
        {

        }


        public Response(T data, string message = null)
        {

            Success = true;
            Message = message;
            Result = data;




        }

        public Response(string message)
        {
            Message = message;
            Success = true;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }
    }
}
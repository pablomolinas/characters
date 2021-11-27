using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public static class Result<T>
    {
       
        public static T SuccessResult(T t)
        {
            return (T)t;
        }

        public static Stack<string> FailureResult(string message)
        {
            return message;
        }
    }

}

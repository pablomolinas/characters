using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ModelsViews.Response
{
    // Hereda de Result, siendo generico me permite incluir cualquier tipo de dato en SuccessResult
    public class Result<T> : Result
    {
        public T Data { get; protected set; }

        protected Result(T t)
        {
            this.Success = true;
            this.Data = t;
        }

        // Metodo estatico para resultado de exito
        public static Result SuccessResult(T t)
        {
            return new Result<T>(t);
        }
    }
}

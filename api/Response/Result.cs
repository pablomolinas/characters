using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Response
{
    // Patron result
    // Proporciona un objeto que estandariza y contiene respuestas success, fail y exception.
    public class Result
    {        
        public bool Success { get; protected set; }
        public string FailureMessage { get; protected set; }
        public Exception Exception { get; protected set; }

        // Constructor para caso de exito
        protected Result()
        {
            this.Success = true;
        }

        // Constructor para situacion de fallo
        protected Result(string message)
        {
            this.Success = false;
            this.FailureMessage = message;
        }

        // Constructor para Exception
        protected Result(Exception ex)
        {
            this.Success = false;
            this.Exception = ex;
        }

        // Metodo estatico para resultado de exito
        public static Result SuccessResult()
        {
            return new Result();
        }

        // Metodo estatico para situacion de fallo con mensaje
        public static Result FailureResult(string message)
        {
            return new Result(message);
        }

        // Metodo estatico para fallo con exception
        public static Result ExceptionResult(Exception ex)
        {
            return new Result(ex);
        }
        public bool IsException()
        {
            return this.Exception != null;
        }        
    }
}

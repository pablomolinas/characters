using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ViewModels.Response;
using api.ViewModels.Auth.Register;
using api.ViewModels.Auth.Login;
using api.Models;

namespace api.Interfaces
{
    public interface IUserService
    {
        public Task<Result> Register(RegisterRequestModel model);
        public Task<Result> Login(LoginRequestModel model);        
    }
}

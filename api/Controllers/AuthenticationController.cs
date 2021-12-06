using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using api.ModelsViews.Auth.Register;
using api.ModelsViews.Auth.Login;
using api.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class authController : ControllerBase
    {
        private readonly IUserService _userService;

        public authController(IUserService userService)
        {
            this._userService = userService;
        }

        //Registro
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            var result = await this._userService.Register(model);
            if (result.Success) 
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);

        }

        //Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var result = await this._userService.Login(model);
            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status401Unauthorized, result);
        }        
    }
}

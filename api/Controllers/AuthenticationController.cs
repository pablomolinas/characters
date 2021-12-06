using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Http;
using api.ModelsViews.Auth.Register;
using api.ModelsViews.Auth.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class authController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public authController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        //Registro
        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            // existe usuario?
            var userExist = await this._userManager.FindByEmailAsync(model.Username);

            // si existe, error
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            // no existe, crear
            var user = new User 
            { 
                UserName = model.Username, 
                Email = model.Email, 
                IsActive = true 
            };
            var result = await this._userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new 
                    { 
                        Status = "Error",
                        Message = $"User creation failed! Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                    });
            }

            return Ok(new { 
                        Status = "Success",
                        Message = $"User created successfully!"
                    });
        }

        //Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            // Chequeo que usuario exista y pass correcta
            var result = await this._signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                var currentUser = await this._userManager.FindByNameAsync(model.Username);
                if (currentUser != null && currentUser.IsActive)
                {
                    // Generar token
                    // Retornar token creado
                    return Ok(await this.GetToken(currentUser));
                }
            }

            return StatusCode(StatusCodes.Status401Unauthorized,
                    new
                    {
                        Status = "Error",
                        Message = $"El usuario {model.Username} no esta autorizado!"
                    });

        }

        private async Task<LoginResponseModel> GetToken(User currentUser)
        {
            var userRoles = await this._userManager.GetRolesAsync(currentUser);

            // generar lista de Claims(privilegios) del usuario
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSignInKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("fSnDFv2kjZAC0MFOWtRvwq6o0Kt5Oym3"));

            var token =  new JwtSecurityToken(
                audience: "https://localhost:44352",
                issuer:"https://localhost:44352",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256));

            return new LoginResponseModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };

        }
    }
}

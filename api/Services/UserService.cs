using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.ViewModels.Auth.Login;
using api.ViewModels.Auth.Register;
using api.ViewModels.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserService(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<Result> Login(LoginRequestModel model)
        {
            try
            { 
                // Chequeo que usuario exista y pass correcta
                var result = await this._signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (result.Succeeded)
                {
                    var currentUser = await this._userManager.FindByNameAsync(model.Username);
                    if (currentUser != null && currentUser.IsActive)
                    {
                        // Generar token, retornar token creado
                        return Result<LoginResponseModel>.SuccessResult(await this.GetToken(currentUser));
                    }
                }
            }
            catch (Exception e)
            {
                return Result.ExceptionResult(e);
            }

            return Result.FailureResult($"El usuario {model.Username} no esta autorizado!");
        }

        public async Task<Result> Register(RegisterRequestModel model)
        {
            try
            { 
                // existe usuario?
                var userExist = await this._userManager.FindByEmailAsync(model.Username);                      

                if (userExist != null) // si existe, error
                {
                    return Result<string>.FailureResult("El usuario ya existe.");
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
                    return Result<string>.FailureResult($"User creation failed! Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

            }
            catch (Exception e)
            {
                return Result.ExceptionResult(e);
            }

            return Result<string>.SuccessResult("User created successfully!");            
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

            var token = new JwtSecurityToken(
                audience: "https://localhost:44352",
                issuer: "https://localhost:44352",
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

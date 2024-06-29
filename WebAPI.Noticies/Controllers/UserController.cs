using Application.Interfaces;
using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Noticies.Models;
using WebAPI.Noticies.Token;

namespace WebAPI.Noticies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUser _IApplicationUsers;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(IApplicationUser IApplicationUsers , UserManager<ApplicationUser> UserManager,
            SignInManager<ApplicationUser> SignInManager)
        {
            _IApplicationUsers = IApplicationUsers;
            _userManager = UserManager;
            _signInManager = SignInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("create-token")]
        public async Task<IActionResult> CreateToken([FromBody]Login login)
        {
            if(string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return Unauthorized();
            }
            var resultado = await _IApplicationUsers.ExisteUsuario(login.Email, login.Password);

            if (resultado)
            {
                var token = new TokenJWTBuilder()
                    .AddSecuritykey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Empresa - V8 TECHNOLOGY LTDA BRAZIL")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("UserAPI", "1")
                    .AddExpiry(15)
                    .Builder();

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("create-user")]
        public async Task<IActionResult>CreateUser([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return Ok("Os Campos de e-mail e senha não podem está vazios...");
            }

            var resultado = await _IApplicationUsers.AdicionarUsuario(login.Email, 
                login.Password, login.Idade, login.Celular);
            if (resultado)
            {
                return Ok("Usuário Criado com Sucesso.");
            }
            else
            {
                return Ok("Erro ao tentar Criar Usuário");
            }

        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("create-token-Identity")]
        public async Task<IActionResult>CreateTokenIdentity([FromBody]Login login){

            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return Unauthorized();
            }
            var resultado = 
                await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure:false);

            if (resultado.Succeeded)
            {
                var token = new TokenJWTBuilder()
                    .AddSecuritykey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Empresa - V8 TECHNOLOGY LTDA BRAZIL")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("UserAPI", "1")
                    .AddExpiry(15)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("create-user-Identity")]
        public async Task<IActionResult>CreateUserIdentity([FromBody] Login login)
        {

            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return Ok("Os Campos de e-mail e senha não podem está vazios...");
            }

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                Celular = login.Celular,
                Tipo = TipoUsuario.Comum,
            };

            var resultado = await _userManager.CreateAsync(user, login.Password);
            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }


            //genereted Authorize Confimation Email.
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // Return Email Authorize
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);
            var StatusMessage = resultado2.Succeeded;

            if (resultado2.Succeeded)
            {
                return Ok("User Created Success.");
            }
            else
            {
                return Ok("Faliure in confirmation Email user");
            }


        }

       
    }
}

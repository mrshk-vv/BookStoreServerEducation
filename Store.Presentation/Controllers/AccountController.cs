using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Tokens;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services;
using Store.Presentation.Providers.Jwt;
using Store.Shared.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly JwtProvider _jwtProvider;
        private readonly IAccountService _accountService;
        private readonly EmailProvider _emailProvider;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, JwtProvider jwtProvider, EmailProvider emailProvider, IConfiguration configuration)
        {
            _accountService = accountService;
            _jwtProvider = jwtProvider;
            _emailProvider = emailProvider;
            _configuration = configuration;
        }


        [HttpPost(Constants.Routes.SIGN_IN_ROUTE)]
        public async Task<IActionResult> SingIn([FromBody]UserSignInModel model)
        {
            await _accountService.SignInAsync(model);
            TokenResponseModel token = await _jwtProvider.GetTokensAsync(model.Email);

            return Ok(token);
        }

        [HttpPost(Constants.Routes.SIGN_UP_ROUTE)]
        public async Task<IActionResult> SingUp([FromBody] UserSingUpModel model)
        {
            await _accountService.CreateUser(model);

            string confToken = await _accountService.GenerateConfirmTokenAsync(model);

            var uriBuilder = new UriBuilder(_configuration["ReturnsPath:ConfirmEmail"]);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["email"] = model.Email;
            query["code"] = confToken;
            uriBuilder.Query = query.ToString();
            var uriString = uriBuilder.ToString();

            await _emailProvider.SendMailAsync(model.Email,
                "Verification Email",
                $"Click this link for complete registration. <a href={uriString}>Click to finish</a>"
            );

            return Ok();
        }

        [HttpPost(Constants.Routes.CONFIRM_EMAIL_ROUTE)]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            code = code.Replace(' ', '+');

            if ((email is null && code is null) || email is null || code is null)
            {
                return BadRequest();
            }
            await _accountService.ConfirmEmailAsync(email, code);

            return Ok();
        }

        [HttpPost(Constants.Routes.SIGN_OUT_ROUTE)]
        public async Task<IActionResult> SingOut()
        {
            await _accountService.SignOutAsync();

            return Ok();
        }

        [HttpPost(Constants.Routes.RESET_PASSWORD_ROUTE)]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordModel model)
        {
            var password = await _accountService.ForgotPasswordAsync(model.Email);
            await _emailProvider.SendMailAsync(model.Email, "Password", $"Your new password {password}");

            return Ok();
        }

        [HttpPost(Constants.Routes.TOKENS_REFRESHING_ROUTE)]
        public async Task<IActionResult> RefreshingTokens([FromBody] TokenRequestModel model)
        {
            TokenResponseModel responseModel = await _jwtProvider.RefreshTokensAsync(model.AccessToken, model.RefreshToken);

            return Ok(responseModel);
        }
    }
}

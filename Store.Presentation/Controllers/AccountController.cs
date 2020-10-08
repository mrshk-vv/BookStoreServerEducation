using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public AccountController(IAccountService accountService, JwtProvider jwtProvider, EmailProvider emailProvider)
        {
            _accountService = accountService;
            _jwtProvider = jwtProvider;
            _emailProvider = emailProvider;
        }


        [HttpPost(Constants.Routes.SIGN_IN_ROUTE)]
        public async Task<IActionResult> SingIn([FromBody] UserSignInModel model)
        {
            await _accountService.SignInAsync(model);
            TokenResponseModel token = await _jwtProvider.GetTokensAsync(model.Email);

            return Ok(token);
        }

        [HttpPost(Constants.Routes.SIGN_UP_ROUTE)]
        public async Task<IActionResult> SingUp([FromBody] UserSingUpModel model)
        {
            await _accountService.SignUpAsync(model);

            string confToken = await _accountService.GenerateConfirmTokenAsync(model);

            var callBackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { email = model.Email, code = confToken },
                protocol: HttpContext.Request.Scheme
            );
            await _emailProvider.SendMailAsync(model.Email,
                "Verification Email",
                $"click this link for complete registration <a href={callBackUrl}>Click to finish</a>"
            );

            return Ok("SingUp complete");
        }

        [HttpGet(Constants.Routes.CONFIRM_EMAIL_ROUTE)]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            await _accountService.ConfirmEmailAsync(email, code);

            return Ok("User complete registration");
        }

        [HttpPost(Constants.Routes.SING_OUT_ROUTE)]
        public async Task<IActionResult> SingOut()
        {
            await _accountService.SignOutAsync();

            return Ok("SingOut complete");
        }

        [HttpPost(Constants.Routes.RESET_PASSWORD)]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordModel model)
        {
            var password = await _accountService.ForgotPasswordAsync(model.Email);
            await _emailProvider.SendMailAsync(model.Email, "Password", $"Your new temp password {password}");

            return Ok("Reset password complete");
        }

        [HttpPost(Constants.Routes.TOKENS_REFRESHING_ROUTE)]
        public async Task<IActionResult> RefreshingTokens([FromBody] TokenRequestModel model)
        {
            TokenResponseModel responseModel = await _jwtProvider.RefreshTokensAsync(model.AccessToken, model.RefreshToken);

            return Ok(responseModel);
        }
    }
}

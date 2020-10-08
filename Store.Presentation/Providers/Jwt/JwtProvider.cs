using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Tokens;
using Store.Shared.Common;
using Store.Shared.Constants;
using Store.Shared.Enums;

namespace Store.Presentation.Providers.Jwt
{
    public class JwtProvider
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly IAccountService _accountService;
        public JwtProvider(IOptions<JwtOptions> jwtOptions, IAccountService accountService)
        {
            _jwtOptions = jwtOptions;
            _accountService = accountService;
        }

        public async Task<TokenResponseModel> GetTokensAsync(string email)
        {
            var jwtOptions = _jwtOptions.Value;
            var securityKey = jwtOptions.GetSymmetricSecurityKey();
            var userClaims = await _accountService.GetUserClaimsAsync(email);

            var token = new JwtSecurityToken(
               issuer: jwtOptions.Issuer,
               audience: jwtOptions.Audience,
               claims: userClaims.Claims,
               notBefore: DateTime.Now,
               expires:DateTime.Now.AddSeconds(jwtOptions.Lifetime),
               signingCredentials: new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256)
            );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = await _accountService.GenerateNewRefreshTokenAsync(email);

            TokenResponseModel tokenResponseModel = new TokenResponseModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return tokenResponseModel;
        }

        public async Task<TokenResponseModel> RefreshTokensAsync(string accessToken, string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var ecryptToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;
            var email = ecryptToken.Claims.Single(e => e.Type == ClaimTypes.Email).Value;
            if (email is null)
            {
                throw new ServerException(Constants.Errors.USER_NOT_FOUND,Enums.Errors.NotFound);
            }

            var oldRefreshToken = await _accountService.GetRefreshTokenAsync(email);

            if (!oldRefreshToken.Equals(refreshToken))
            {
                throw new ServerException(Constants.Errors.REFRESH_TOKEN_NOT_EQUALS, Enums.Errors.NotFound);
            }

            return await GetTokensAsync(email);
        }
    }
}

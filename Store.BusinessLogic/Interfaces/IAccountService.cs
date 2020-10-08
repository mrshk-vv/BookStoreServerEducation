using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        #region Administration
        public Task<IEnumerable<UserModel>> GetUsers();
        public Task<UserModel> GetUserByIdAsync(string id);
        public Task UpdateUserAsync(UserModel model);
        public Task DeleteUserAsync(string id);
        public Task BlockUserAsync(string id);
        #endregion

        #region Authentiation and authorization
        public Task SignUpAsync(UserSingUpModel userModel);
        public Task<string> GenerateConfirmTokenAsync(UserSingUpModel user);
        public Task ConfirmEmailAsync(string email, string token);
        public Task SignInAsync(UserSignInModel userModel);
        public Task SignOutAsync();
        public Task<string> ForgotPasswordAsync(string email);
        #endregion

        #region JwtProvider
        public Task<ClaimsIdentity> GetUserClaimsAsync(string email);
        public Task<string> GetRefreshTokenAsync(string email);
        public Task<string> GenerateNewRefreshTokenAsync(string email);
        #endregion
    }
}

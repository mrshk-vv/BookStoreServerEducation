﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        #region Administration
        public Task<IEnumerable<UserModel>> GetUsersAsync();
        public Task<IEnumerable<UserModel>> GetUsersAsync(PaginationQuery paginationQuery,UsersFilter filter);
        public Task<UserModel> GetUserByIdAsync(string id);
        public Task<UserModel> UpdateUserAsync(UserModel model);
        public Task DeleteUserAsync(string id);
        public Task<UserModel> BlockUserAsync(string id);
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

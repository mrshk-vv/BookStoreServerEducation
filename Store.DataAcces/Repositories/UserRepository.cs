using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Filters;

namespace Store.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Working with UserList 
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(int skip, int pageSize)
        {
            return await _userManager.Users.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(int skip, int pageSize, UsersFilter filter)
        {
            return await _userManager.Users.Where(user =>
                user.IsBlocked == filter.Status ||
                user.FirstName == GetUserFirstName(filter.UserName) ||
                user.LastName == GetUserLastName(filter.UserName)).Skip(skip).Take(pageSize).ToListAsync();
        }

        private string GetUserFirstName(string fullName)
        {
            var fullNameArray = fullName.Split(' ');

            return fullNameArray[0];
        }

        private string GetUserLastName(string fullName)
        {
            var fullNameArray = fullName.Split(' ');

            return fullNameArray[1];
        }

        #endregion


        public async Task<User> GetUserAsync(User entity)
        {
            return await _userManager.FindByEmailAsync(entity.Email);
        }

        public async Task<User> GetUserAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User entity)
        {
            await _userManager.CreateAsync(entity);

            return entity;
        }

        public async Task<User> RemoveUserAsync(User entity)
        {
            await _userManager.DeleteAsync(entity);

            return entity;
        }

        public async Task<User> UpdateUserAsync(User entity)
        {
            await _userManager.UpdateAsync(entity);

            return entity;
        }

        public async Task<bool> CreateAsync(User entity, string password)
        {
            var res = await _userManager.CreateAsync(entity, password);
            if(!res.Succeeded)
            {
                return false;
            }

            return res.Succeeded;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> BlockUserAsync(User user)
        {
            user.IsBlocked = true;
            await _userManager.UpdateAsync(user);

            return user;
        }

        #region Authorization
        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<string> GenerateConfirmTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task ConfirmEmailAsync(User user, string token)
        {
            await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateResetPasswordTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task ResetPasswordAsync(User user, string token, string newPassword)
        {
            await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<string> GetRefreshTokenAsync(User user)
        {
            return await _userManager.GetAuthenticationTokenAsync(user, "StoreServer", "RefreshToken");
        }

        public async Task<string> GenerateNewRefreshTokenAsync(User user)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, "StoreServer", "RefreshToken");
            var refreshToken = await _userManager.GenerateUserTokenAsync(user, "StoreServer", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(user, "StoreServer", "RefreshToken", refreshToken);

            return refreshToken;
        }
        #endregion
    }
}

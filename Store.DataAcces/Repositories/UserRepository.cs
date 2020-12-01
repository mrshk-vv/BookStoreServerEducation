using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Enums;
using Store.Shared.Filters;

namespace Store.DataAccess.Repositories
{
    public class UserRepository : BaseEfRepository<User>,IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(ApplicationContext context,UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : base(context)
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
            var firstName = GetUserFirstName(filter.Email);
            var lastName = GetUserLastName(filter.Email);

            var list = await _userManager.Users
                .Where(u => u.FirstName == firstName && u.LastName == lastName)
                .Skip(skip).Take(pageSize).ToListAsync();

            return list;

        }

        public async Task DeleteUserAsync(User entity)
        {
            await _userManager.DeleteAsync(entity);
        }

        public async Task<User> ChangeUserBlockStatusAsync(User user)
        {
            user.IsBlocked = !user.IsBlocked;
            await _userManager.UpdateAsync(user);

            return user;
        }

        private string GetUserFirstName(string fullName)
        {
            if (fullName != null)
            {
                var fullNameArray = fullName.Split(' ');
                Console.WriteLine(fullNameArray[0]);

                return fullNameArray[0];
            }

            return null;
        }

        private string GetUserLastName(string fullName)
        {
            if (fullName != null)
            {
                var fullNameArray = fullName.Split(' ');
                if (fullNameArray.Length > 1)
                {
                    Console.WriteLine(fullNameArray[1]);
                    return fullNameArray[1];
                }

                return null;
            }

            return null;
        }

        #endregion


        #region User Managment

        public async Task<User> CreateUserAsync(User entity, string password)
        {
            var res = await _userManager.CreateAsync(entity, password);
            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(entity, Enums.Roles.Client.ToString());
                return entity;
            }

            return entity;
        }

        public async Task<User> UpdateUserAsync(User entity, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == entity.Id);

            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.NormalizedEmail = entity.Email.ToUpper();
            user.UserName = entity.Email;
            user.NormalizedUserName = entity.Email.ToUpper();
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return entity;
        }

        #endregion


        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
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

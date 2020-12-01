using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;
using Store.Shared.Filters;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User entity, string password);
        public Task<User> GetUserByIdAsync(string id);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<IEnumerable<User>> GetAllUsersAsync(int skip, int pageSize);
        public Task<IEnumerable<User>> GetAllUsersAsync(int skip, int pageSize, UsersFilter filter);
        public Task<User> UpdateUserAsync(User user, string password);
        public Task DeleteUserAsync(User user);
        public Task<User> ChangeUserBlockStatusAsync(User user);


        public Task<IList<string>> GetUserRolesAsync(User user);
        public Task<string> GenerateConfirmTokenAsync(User user);
        public Task ConfirmEmailAsync(User user, string token);
        public Task<string> GenerateResetPasswordTokenAsync(User user);
        public Task ResetPasswordAsync(User user, string token, string newPassword);
        public Task<string> GetRefreshTokenAsync(User user);
        public Task<string> GenerateNewRefreshTokenAsync(User user);
    }
}

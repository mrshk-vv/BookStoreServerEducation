using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository<T> : IBaseRepository<T> where T : class
    {
        public Task<bool> CreateAsync(T entity, string password);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<User> BlockUserAsync(User user);


        public Task<IList<string>> GetUserRolesAsync(User user);
        public Task<string> GenerateConfirmTokenAsync(User user);
        public Task ConfirmEmailAsync(User user, string token);
        public Task<string> GenerateResetPasswordTokenAsync(User user);
        public Task ResetPasswordAsync(User user, string token, string newPassword);
        public Task<string> GetRefreshTokenAsync(User user);
        public Task<string> GenerateNewRefreshTokenAsync(User user);
    }
}

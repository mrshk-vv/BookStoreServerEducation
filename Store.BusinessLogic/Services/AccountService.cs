using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Common;
using Store.Shared.Constants;
using Store.Shared.Enums;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountService(IUserRepository userRepository, SignInManager<User> signInManager, IMapper mapper,
            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        #region User Managment
        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserModel>>(await _userRepository.GetAllUsersAsync());
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync(PaginationQuery paginationQuery, UsersFilter filter)
        {
            var skip = (paginationQuery.PageNumber - 1) * paginationQuery.PageSize;

            if (filter is null)
            {
                return _mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(
                    await _userRepository.GetAllUsersAsync(skip, paginationQuery.PageSize));
            }

            var userList = _mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(
                await _userRepository.GetAllUsersAsync(skip, paginationQuery.PageSize,filter));

            return userList;
        }

        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userModel = _mapper.Map<User, UserModel>(user);

            return userModel;
        }
        public async Task<UserModel> CreateUser(UserSingUpModel userModel)
        {
            var user = _mapper.Map<UserSingUpModel, User>(userModel);
            user = await _userRepository.CreateUserAsync(user, userModel.Password);
            if (user is null)
            {
                throw new ServerException(Constants.Errors.CREATE_USER_FAILED, Enums.Errors.BadRequest);
            }

            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> UpdateUserAsync(UserSingUpModel model)
        {
            var user = await _userRepository.UpdateUserAsync(
                _mapper.Map<UserSingUpModel, User>(model),
                model.Password);

            return _mapper.Map<User, UserModel>(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user is null)
            {
                throw new ServerException(Constants.Errors.USER_NOT_FOUND, Enums.Errors.BadRequest);
            }
            await _userRepository.DeleteUserAsync(user);
        }

        public async Task<UserModel> ChangeUserBlockStatusAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<User,UserModel>(await _userRepository.ChangeUserBlockStatusAsync(user));
        }
        #endregion

        #region Authentiation and authorization
        public async Task<string> GenerateConfirmTokenAsync(UserSingUpModel user)
        {
            User currentUser = await _userRepository.GetUserByEmailAsync(user.Email);
            return await _userRepository.GenerateConfirmTokenAsync(currentUser);
        }

        public async Task ConfirmEmailAsync(string email, string token)
        {
            if (email is null || token is null)
            {
                throw new ServerException(Constants.Errors.EMPTY_FIELD, Enums.Errors.BadRequest);
            }

            User user = await _userRepository.GetUserByEmailAsync(email);

            if (user is null)
            {
                throw new ServerException(Constants.Errors.USER_NOT_FOUND, Enums.Errors.NotFound);
            }

            await _userRepository.ConfirmEmailAsync(user, token);

        }

        public async Task SignInAsync(UserSignInModel userModel)
        {
            User user = await _userRepository.GetUserByEmailAsync(userModel.Email);
            if (user is null)
            {
                throw new ServerException(Constants.Errors.USER_NOT_FOUND, Enums.Errors.NotFound);
            }

            if (user.IsBlocked)
            {
                throw new ServerException(Constants.Errors.USER_IS_BLOCKED, Enums.Errors.Unauthorized);
            }

            var result =
                await _signInManager.PasswordSignInAsync(user, userModel.Password, userModel.IsRemember, false);
            if (!result.Succeeded)
            {
                throw new ServerException(Constants.Errors.INVALID_CREDENTIALS, Enums.Errors.Unauthorized);
            }
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user is null)
            {
                throw new ServerException(Constants.Errors.USER_NOT_FOUND, Enums.Errors.BadRequest);
            }

            var token = await _userRepository.GenerateResetPasswordTokenAsync(user);
            string password = GenerateTempPassword();

            await _userRepository.ResetPasswordAsync(user, token, password);

            return password;
        }
        #endregion

        #region ForJwt
        public async Task<ClaimsIdentity> GetUserClaimsAsync(string email)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user is null)
            {
                throw new ServerException(Constants.Errors.USER_NOT_FOUND, Enums.Errors.Unauthorized);
            }

            var userRoles = await _userRepository.GetUserRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim("id", user.Id),
                new Claim("email", user.Email),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
            };

            claims.AddRange(userRoles.Select(item => new Claim("role", item)));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        public async Task<string> GetRefreshTokenAsync(string email)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user is null)
            {
                throw new ServerException(Constants.Errors.USER_NOT_FOUND, Enums.Errors.NotFound);
            }

            return await _userRepository.GetRefreshTokenAsync(user);
        }

        public async Task<string> GenerateNewRefreshTokenAsync(string email)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user is null)
            {
                throw new ServerException(Constants.Errors.USER_NOT_FOUND, Enums.Errors.NotFound);
            }

            return await _userRepository.GenerateNewRefreshTokenAsync(user);
        }
        #endregion

        #region PasswordGenerator
        private static string GenerateTempPassword()
        {
            string password = GeneratePassword();
            if (Regex.IsMatch(password, Constants.Password.PASSWORD_PATERN))
            {
                password = GeneratePassword();
            }

            return password;
        }

        private static string GeneratePassword()
        {
            string password = string.Empty;
            int[] tempPassword = new int[Constants.Password.PASSWORD_LENGHT];
            Random random = new Random();
            for (int i = 0; i < tempPassword.Length; i++)
            {
                tempPassword[i] = random.Next(Constants.Password.SIMBOLS_RANGE_START,
                    Constants.Password.SIMBOLS_RANGE_END);
                password += (char)tempPassword[i];
            }

            return password;
        }
        #endregion

    }
}

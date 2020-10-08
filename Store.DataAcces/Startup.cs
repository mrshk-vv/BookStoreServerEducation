using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialize;
using Store.DataAccess.Repositories;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess
{
    public static class Startup
    {
        private const string allowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz1234567890";

        public static async Task DataAccessInitializer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>(opts =>
                {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                    
                    opts.User.RequireUniqueEmail = true;
                    opts.User.AllowedUserNameCharacters = allowedUserNameCharacters;
                    opts.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider("StoreServer", typeof(DataProtectorTokenProvider<User>));

            services.AddTransient(typeof(IUserRepository<User>), typeof(UserRepository));
            services.AddTransient(typeof(IAuthorRepository<Author>), typeof(AuthorRepository));
            services.AddTransient(typeof(IPrintingEditionRepository<PrintingEdition>), typeof(PrintingEditionRepository));

            await services.BuildServiceProvider().IdentityInitializerAsync();



        }
    }
}

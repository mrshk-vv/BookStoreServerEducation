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
                    opts.Password.RequiredLength = 8;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                    
                    opts.User.RequireUniqueEmail = true;
                    opts.SignIn.RequireConfirmedEmail = true;
                    opts.User.AllowedUserNameCharacters = allowedUserNameCharacters;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider("StoreServer", typeof(DataProtectorTokenProvider<User>));

            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPrintingEditionRepository, PrintingEditionRepository>();
            services.AddScoped<IAuthorInPERepository, AuthorInPERepository>();
            services.AddTransient(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddTransient(typeof(IOrderItemRepository), typeof(OrderItemRepository));
            services.AddTransient(typeof(IPaymentRepository), typeof(PaymentRepository));

            await services.BuildServiceProvider().IdentityInitializerAsync();



        }
    }
}

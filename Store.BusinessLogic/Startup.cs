using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Mapping;
using Store.BusinessLogic.Services;
using Store.DataAccess;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Common;


namespace Store.BusinessLogic
{
    public static class Startup
    {
        public static void BusinessLogicInitializer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpOptions>(configuration.GetSection("SmtpOptions"));

            services.AddSingleton(typeof(EmailProvider));
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient(typeof(IAuthorService), typeof(AuthorService));
            services.AddTransient(typeof(IPrintingEditionService), typeof(PrintingEditionService));

            services.DataAccessInitializer(configuration);

            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new UserMapping());
                configuration.AddProfile(new AuthorMapping());
                configuration.AddProfile(new PrintingEditionMapping());
            });

            var mapper = mapperConfig.CreateMapper();

            //Inject dependencies
            services.AddSingleton(mapper);

        }
    }
}
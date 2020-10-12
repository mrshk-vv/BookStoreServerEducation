using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Interfaces;
using Store.BusinessLogic.Mapping;
using Store.BusinessLogic.Services;
using Store.DataAccess;
using Store.DataAccess.Repositories;
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
            services.AddTransient(typeof(IAuthorInPEService), typeof(AuthorInPEService));
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absolureUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
                return new UriService(absolureUri);
            });

            services.DataAccessInitializer(configuration);

            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new UserMapping());
                configuration.AddProfile(new AuthorMapping());
                configuration.AddProfile(new PrintingEditionMapping());
                configuration.AddProfile(new AuthorInPEMapping());
            });

            var mapper = mapperConfig.CreateMapper();

            //Inject dependencies
            services.AddSingleton(mapper);

        }
    }
}
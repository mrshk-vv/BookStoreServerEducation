using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic;
using Store.Presentation.Extentions;
using Store.Presentation.Providers.Jwt;
using Store.Shared.Common;

namespace Store.Presentation
{
    public class Startup
    {
        private IConfiguration configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var authOptions = configuration.GetSection("JwtOptions");
            var jwtConf = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.Configure<JwtOptions>(authOptions);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = jwtConf.Audience,
                        ValidIssuer = jwtConf.Issuer,

                        ValidateLifetime = true,

                        IssuerSigningKey = jwtConf.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin();
                    }
                );
            });

            services.AddSwaggerGen();

            services.AddTransient(typeof(JwtProvider));

            services.BusinessLogicInitializer(configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //Logger
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            loggerFactory.CreateLogger("logger.txt");
            //---------------------

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //---------------------
            

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

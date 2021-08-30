using System.Text;
using API.Data;
using API.helpers;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class StartUpExtensions
    {
        public static IServiceCollection StartUpExten(this IServiceCollection services, IConfiguration config)
        {
            
            services.AddScoped<IUserRepository, UserRepository>();//rendo disponibile a tutte le classi dell'applicazione
            //un interfaccia e la sua implementazione. La injecto nel costruttore tramite l'interfaccia
            services.AddScoped<ITokenService, TokenService>();//serve per inject TokenService all'interno di un controller
            //verrà usata dentro il metodo login del AccountController

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {

                options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                };

            });
            services.AddScoped<ICloudinary,PhotoService>();
            services.AddScoped<LogUserActivity>();
            return services;
        }
    }
}
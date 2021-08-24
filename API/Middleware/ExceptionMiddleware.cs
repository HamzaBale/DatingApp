using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)//logger per stampare nella console e RequesteDelegate per passare il comando al prossimo middleware.
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context){
            try{
                await this.next(context);
            }
            catch(Exception ex){
                this.logger.LogError(ex,ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.ToString());
            }
        }
    }
}
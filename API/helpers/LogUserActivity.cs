using System;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
namespace API.helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultcontext = await next(); //aspettiamo che la chiamata http abbia finito prima di 
            //modificare LastActive.
            if(!resultcontext.HttpContext.User.Identity.IsAuthenticated) return;
            var userId = resultcontext.HttpContext.User.GetUserId();
            var repo = resultcontext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user =await repo.GetUserByIdAsync(userId);
            user.LastActive = DateTime.Now;
            await repo.SaveAllAsync();
        }
    }
}
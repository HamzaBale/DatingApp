using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsExtensions
    {
            public static string GetUsername(this ClaimsPrincipal user ){
            
                return user.FindFirst(ClaimTypes.Name)?.Value;

            }
               public static int GetUserId(this ClaimsPrincipal user ){
                   var id =  user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            }
    }
}
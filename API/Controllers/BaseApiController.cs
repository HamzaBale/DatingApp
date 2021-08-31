using API.Data;
using API.helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{ 
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LogUserActivity))]
    public class BaseApiController : ControllerBase
    {
    }
}
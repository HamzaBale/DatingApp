using API.Data;
using API.helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{ 
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LogUserActivity))] // usata per cambiare valore al lastactive.
    //in pratica la classe loguseractivity, aspetta che arrivi e finisca una richiesta http per poi
    //controllare se effettivamente l'utente era loggato, se lo è => aggiorno colonna last active di quel user.
    //Ad ogni richiesta http.
    public class BaseApiController : ControllerBase
    {
    }
}
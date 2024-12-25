using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthenticationAuthorizationCore.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet("index")] // This matches the route '/home/index'
        public string Index() => "Index Route";

        [HttpGet("secret")] // This matches the route '/home/secret'
        //[Authorize(Roles = "admin")]
        public string Secret() => "Secret Route";
    }
}

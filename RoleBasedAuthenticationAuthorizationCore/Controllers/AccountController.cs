using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace RoleBasedAuthenticationAuthorizationCore.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Find the user by email
            //var user = await _userManager.GetUserName()
            var user = await _userManager.FindByEmailAsync(email.ToLower());
            var role  = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                // Handle user not found (e.g., return an error message)
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Ok();
            }

            // Sign in the user
            var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Redirect the user to a specific page on successful login
                return RedirectToAction("Index", "Home");
            }

            // If we got here, something went wrong
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Ok();
        }
    }

    //=>
    //SignIn(
    //    new ClaimsPrincipal(
    //        new ClaimsIdentity(
    //            new Claim[] {
    //                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
    //                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString())
    //            }

    //        )
    //    )
    //);

}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulPetApi.Authentication;
using RestfulPetApi.Data;
using RestfulPetApi.Models;

namespace RestfulPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly AppDbContext _context;
        public LoginController(JwtAuthenticationManager jwtAuthenticationManager, AppDbContext context)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
        }


        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser(LoginModel usr)
        {
            List<User> users = _context.Users.ToList();

            var token = jwtAuthenticationManager.Authenticate(usr.UserName, usr.PasswordHash, users);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("TestRoute")]
        [HttpGet]
        public IActionResult test()
        {
            return Ok("Authorized");
        }
    }
}

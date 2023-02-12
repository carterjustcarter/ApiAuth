using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticationAPI.Authorization;
using UserAuthenticationAPI.Model;

namespace UserAuthenticationAPI.Controllers {
    [ApiController]
    [Route("api")]
    public class UserController : Controller {
        [HttpGet]
        [Permission(nameof(AccessLevel.BASIC_ACCESS))]
        public IActionResult GetUsers() {
            return Ok(Container.GetUsers());
        }

        [HttpGet("{id}")]
        [Permission(nameof(AccessLevel.ADMIN_ACCESS))]
        [Authorize]
        public IActionResult GetUser(int id) {
            var user = Container.GetUserById(id);
            if(user != null)
                return Ok(user);

            return BadRequest("User does not exists");
        }
    }
}

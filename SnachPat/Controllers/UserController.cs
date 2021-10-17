using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnachPat.Models;
using SnachPat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnachPat.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationManager _JWTAuthenticationManager;

        public UserController(IAuthenticationManager JWTAuthenticationManager)
        {
            _JWTAuthenticationManager = JWTAuthenticationManager;
        }
        [HttpPost("Authenticate")]
        public IActionResult Authentication([FromBody] User user)
        {
            var token = _JWTAuthenticationManager.Authenticate(user.User_name, user.User_passwd);
            if (token is null)
            {
                return Unauthorized();
            }
            return Ok();
        }
        [Authorize]
        [HttpGet("Secured")]
        public string Secured()
        {
            return "THIS IS SECURED";
        }
    }
}

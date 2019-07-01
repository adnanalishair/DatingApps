using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datingapp.API.Data;
using Datingapp.API.Dtoclasses;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Datingapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController: ControllerBase
    {
        private readonly IAutrepostory _repo;
        public AuthController(IAutrepostory repo)
        {
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registermodel)
        {
            registermodel.Username =  registermodel.Username.ToLower();
            if(await _repo.UserExist(registermodel.Username))
            {
                return BadRequest("User Already Exist");
            }
            var usertocreate = new User{
                   Username= registermodel.Username
            };
            var CreatedUser = _repo.Register(usertocreate , registermodel.Password);


            return StatusCode(201);
        }

    }
}
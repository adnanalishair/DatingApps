using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datingapp.API.Data;
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

    }
}
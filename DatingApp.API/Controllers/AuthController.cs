using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthorRepository repo;

        public AuthController(IAuthorRepository repo)
        {
            this.repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegsiterDto model)
        {

            model.UserName = model.UserName.ToLower();
            if(await repo.UserExist(model.UserName))
            return BadRequest("User Already Exist");
            var userToCreate = new NewUser()
            {
                Name = model.UserName
            
            };
            var createUser = repo.Register(userToCreate ,model.PassWord);
            return StatusCode(201);
        }
        
    }
}
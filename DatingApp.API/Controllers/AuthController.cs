using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthorRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthorRepository repo,IConfiguration configuration )
        {
            _repo = repo;
            _config = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegsiterDto model)
        {

            model.UserName = model.UserName.ToLower();
            if(await _repo.UserExist(model.UserName))
            return BadRequest("User Already Exist");
            var userToCreate = new NewUser()
            {
                Name = model.UserName
            
            };
            var createUser = _repo.Register(userToCreate ,model.PassWord);
            return StatusCode(201);
        }

        public async Task<IActionResult> Login(UserForLoginDto model)
        {
            var userFromRepo = await _repo.Login(model.Name,model.Password);
            if(userFromRepo == null)
            return Unauthorized();

            var claims = new []{
                new Claim(ClaimTypes.NameIdentifier,model.Id.ToString()),
                new Claim(ClaimTypes.Name,model.Name)
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512);

            var tokenDesription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds

            };

            var tokenHandelr = new JwtSecurityTokenHandler();

            var token = tokenHandelr.CreateToken(tokenDesription);


            return Ok(new{
                token = tokenHandelr.WriteToken(token)
            } );


        }
         
        
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public ITokenRepository TokenRepository { get; }

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            TokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                //add role to the user
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if(identityResult.Succeeded) 
                    {
                        return Ok("User Register succefully");
                    }
                }
            }
            return BadRequest("Something went wrong"); 

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
           var user =  await userManager.FindByEmailAsync(loginRequest.Username);
            if(user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (checkPassword)
                {
                    var roles = await userManager.GetRolesAsync(user);  
                    if (roles != null)
                    {
                        var jwtToken = TokenRepository.CreateJWTToken(user , roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response); 
                    }
                }

            }
             return BadRequest("Wrong credential");
            
        }
    }

    
}

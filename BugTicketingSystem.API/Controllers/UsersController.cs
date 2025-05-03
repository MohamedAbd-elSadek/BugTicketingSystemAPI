using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BugTicketingSystem.BL.Dtos.AuthDtos;
using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Dtos.UserDtos;
using BugTicketingSystem.BL.Validators.UserValidators;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace BugTicketingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UsersController(IConfiguration configuration,
            UserManager<User> userManager,
            IStringLocalizer<SharedResources> localizer)
        {
            _configuration = configuration;
            _userManager = userManager;
            _localizer = localizer;
        }
        #region Login
        [HttpPost("login")]
        public async Task<Results<Ok<TokenDto>, UnauthorizedHttpResult>>
        Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return TypedResults.Unauthorized();
            }
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                return TypedResults.Unauthorized();
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var tokenDto = GenerateToken(claims.ToList());
            return TypedResults.Ok(tokenDto);

        }
        #endregion
        #region Register
        [HttpPost("register")]
        public async Task<ActionResult<GeneralResult>>
            Register([FromBody] UserAddDto userAddDto, [FromServices] UserAddValidator validator)
        {
            var validationResult = await validator.ValidateAsync(userAddDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new GeneralResult
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(e => new ResultError
                    {
                        Code = e.ErrorCode,
                        Message = e.ErrorMessage
                    }).ToArray()
                });
            }
            var role = Enum.Parse<UserRole>(userAddDto.Role, true);

            var user = new User()
            {
                UserName = userAddDto.Name,
                Email = userAddDto.Email,
                Name = userAddDto.Name,
                Role = role
            };
            var result = await _userManager.CreateAsync(user, userAddDto.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new ResultError
                {
                    Code = "IDENTITY_ERROR",
                    Message = _localizer[e.Code] ?? e.Description 
                }).ToArray();

                return BadRequest(new GeneralResult { IsSuccess = false, Errors = errors });
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            await _userManager.AddClaimsAsync(user, claims);
            return Ok(new GeneralResult { IsSuccess = true });
        }
        #endregion
        #region Generate token
        private TokenDto GenerateToken(List<Claim> claims)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyBytes);

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                
                claims: claims,
                expires: expires,
                signingCredentials: creds);
            var ToKenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenDto(ToKenString, token.ValidTo);
        }
        #endregion

        
    }
}

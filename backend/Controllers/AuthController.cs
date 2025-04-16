using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Backend.Models.DTOs;

namespace Backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
      if (model.Password != model.ConfirmPassword)
        return BadRequest("Passwords do not match");

      var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        return Ok(new { Message = "User created successfully" });
      }

      return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null)
        return Unauthorized();

      var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
      if (!isValidPassword)
        return Unauthorized();

      var token = GenerateJwtToken(user);

      return Ok(new AuthResponseDto
      {
        Token = token,
        User = new UserDto
        {
          Id = user.Id,
          Email = user.Email!
        }
      });
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
        {
            throw new InvalidOperationException("JWT_KEY must be at least 32 characters long");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(7);

        var token = new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
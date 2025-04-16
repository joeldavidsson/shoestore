namespace Backend.Models.DTOs
{
  public class LoginDto
  {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }

  public class RegisterDto
  {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
  }

  public class AuthResponseDto
  {
    public string Token { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
  }

  public class UserDto
  {
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
  }
}
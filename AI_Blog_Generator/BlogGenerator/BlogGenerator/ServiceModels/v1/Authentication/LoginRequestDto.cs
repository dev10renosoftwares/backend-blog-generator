namespace BlogGenerator.ServiceModels.v1.Authentication;

public class LoginRequestDto
{
    public string Email { get; set; }

    public string Password { get; set; }
}
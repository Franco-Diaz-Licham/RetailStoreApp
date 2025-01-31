namespace RetailStore.Server.Models.Dtos;

public class UserDto
{
    public UserDto(){ }
    public UserDto(string email, string displayName, string token)
    {
        Email = email;
        DisplayName = displayName;
        Token = token;
    }

    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string Token { get; set; }
}

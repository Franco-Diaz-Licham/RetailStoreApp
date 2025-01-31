namespace RetailStore.Server.Models.Entities;

public class UserEntity : IdentityUser
{
    public UserEntity(){ }

    public UserEntity(string email, string displayName, string userName)
    {
        DisplayName = displayName;
        Email = email;
        UserName = userName;
    }

    public string DisplayName { get; set; }
    public AddressEntity Address { get; set; }
}
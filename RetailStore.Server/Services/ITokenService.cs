namespace RetailStore.Server.Services;

public interface ITokenService
{
    string CreateToken(UserEntity user);
}

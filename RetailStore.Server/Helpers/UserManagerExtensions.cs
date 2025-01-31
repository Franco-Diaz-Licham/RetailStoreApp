namespace RetailStore.Server.Helpers;

public static class UserManagerExtensions
{
    public static async Task<UserEntity?> FindUserByClaimsPrincipleWithAddress(this UserManager<UserEntity> userManager, ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);
        var output = await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        return output;
    }

    public static async Task<UserEntity?> FindByEmailFromClaimsPrincipal(this UserManager<UserEntity> userManager, ClaimsPrincipal user)
    {
        var output = await userManager.Users.SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        return output;
    }
}

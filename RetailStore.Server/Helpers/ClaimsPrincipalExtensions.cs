namespace RetailStore.Server.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static string? RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
    {
        var output = user.FindFirstValue(ClaimTypes.Email);
        return output;
    }
}
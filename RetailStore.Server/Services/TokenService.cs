namespace RetailStore.Server.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config, UserManager<UserEntity> userManager)
    {
        string key = config.GetValue<string>("Token:Key")!;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        _userManager = userManager;
        _config = config;
    }

    public string CreateToken(UserEntity user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email!.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.DisplayName.ToString())
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescrp = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config.GetValue<string>("Token:Issuer")!
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescrp);

        return tokenHandler.WriteToken(token);
    }
}

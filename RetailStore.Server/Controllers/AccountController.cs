namespace RetailStore.Server.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ITokenService tokenService, IMapper mapper)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        if (user == null) return NotFound(new ApiResponse(404));
        return new UserDto(user.Email!, user.DisplayName, _tokenService.CreateToken(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
        var output = new UserDto(user.Email!, user.DisplayName, _tokenService.CreateToken(user));
        return output;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
        {
            var errors = new[] { "Email address is in use" };
            return BadRequest(new ApiValidationErrorResponse(errors));
        }

        var user = new UserEntity(registerDto.Email, registerDto.DisplayName, registerDto.Email);
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        var output = new UserDto(user.Email!, user.DisplayName, _tokenService.CreateToken(user));
        return output;
    }

    [HttpGet("emailExists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> GetUserAddress()
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
        if (user == null) return NotFound(new ApiResponse(404));
        return _mapper.Map<AddressEntity, AddressDto>(user.Address);
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
        if (user == null) return NotFound(new ApiResponse(404));

        user.Address = _mapper.Map<AddressDto, AddressEntity>(address);
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded) return Ok(_mapper.Map<AddressDto>(user.Address));
        return BadRequest("Problem updating the user...");
    }
}

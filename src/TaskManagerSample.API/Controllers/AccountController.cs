using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagerSample.API.Extensions;
using TaskManagerSample.API.ViewModels;
using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Core.Models;

namespace TaskManagerSample.API.Controllers;

[Route("api/account")]
public class AccountController : MainController
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;
    private readonly ILogger _logger;

    public AccountController(INotifier notifier,
                             IUser user,
                             IOptions<AppSettings> appSettings,
                             IUserRepository userRepository,
                             IUserService userService,
                             IMapper mapper,
                             ILogger<AccountController> logger) : base(notifier, user)
    {
        _appSettings = appSettings.Value;
        _userRepository = userRepository;
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserViewModel loginUser)
    {
        var userGet = await _userRepository.GetByFilter(x => x.Email.Equals(loginUser.Email) && x.Password.Equals(loginUser.Password));
        var user = userGet.FirstOrDefault();

        if (user == null) return NotFound();

        return CustomResponse(await GenerateJwt(user));
    }

    private async Task<LoginResponseViewModel> GenerateJwt(User user)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        claims.Add(new Claim("role", user.Role));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.HoursExpiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        var response = new LoginResponseViewModel
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.HoursExpiration).TotalSeconds,
            UserToken = new UserTokenViewModel
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
            }
        };

        return response;
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}
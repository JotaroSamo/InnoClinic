using Auth_API.Contract;
using Auth_API.Domain.Abstract.Service;
using Auth_API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auth_API.Controllers
{
    using Global;
    using Global.Dto;
    using MassTransit;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly JwtOptions _jwtOptions;
        private readonly ILogger<AuthController> _logger;
        private readonly IBus _bus;

        public AuthController(IUserService userService, IOptions<JwtOptions> jwtOptions, ILogger<AuthController> logger, IBus bus)
        {
            _userService = userService;
            _jwtOptions = jwtOptions.Value;
            _logger = logger;
            _bus = bus;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRequest)
        {
            _logger.LogInformation("Registering user with email {Email}", userRequest.Email);
            var user = await _userService.RegisterAsync(userRequest.Email, userRequest.Password);

            if (user.IsFailure)
            {
                _logger.LogWarning("Registration failed for user with email {Email}: {Error}", userRequest.Email, user.Error);
                return BadRequest(user.Error);
            }
            _logger.LogInformation("User registered successfully with email {Email}", userRequest.Email);
            var message = new AccountDto
            {
                UserId = user.Value.Id, // Предположим, что у вас есть Id пользователя
                Email = user.Value.Email,
                Password = user.Value.HashPassword,
            };

            //await _bus.Publish(message); // Используйте _bus для отправки сообщения
            return Ok(user.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            _logger.LogInformation("Logging in user with email {Email}", model.Email);
            var tokens = await _userService.LoginAsync(model.Email, model.Password);
            if (tokens.IsFailure)
            {
                _logger.LogWarning("Login failed for user with email {Email}: {Error}", model.Email, tokens.Error);
                return BadRequest(tokens.Error);
            }
            _logger.LogInformation("User logged in successfully with email {Email}", model.Email);
            Response.Cookies.Append("AccessToken", tokens.Value.AccessToken);
            Response.Cookies.Append("RefreshToken", tokens.Value.RefreshToken);
            return Ok(new { tokens.Value.AccessToken, tokens.Value.RefreshToken });
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            _logger.LogInformation("Refreshing tokens");
            string ascessToken = Request.Cookies["AccessToken"];
            string refreshToken = Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(ascessToken) || string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogWarning("Tokens not found");
                return BadRequest("Tokens not found");
            }
            var tokens = await _userService.RefreshToken(ascessToken, refreshToken);
            Response.Cookies.Append("AccessToken", tokens.Value.AccessToken);
            Response.Cookies.Append("RefreshToken", tokens.Value.RefreshToken);
            _logger.LogInformation("Tokens refreshed successfully");
            return Ok( new { tokens.Value.AccessToken, tokens.Value.RefreshToken });
        }

        [HttpGet("GetUsers")]
        //[Authorize]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation("Getting list of users");
            var user = await _userService.GetUsers();
            return Ok(user);
        }
    }

}

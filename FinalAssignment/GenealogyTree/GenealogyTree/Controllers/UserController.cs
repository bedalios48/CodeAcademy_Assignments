using AutoMapper;
using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GenealogyTree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordService passwordService,
            IMapper mapper, 
            ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordService = passwordService;
            _mapper = mapper;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            _logger.LogInformation($"Logging in for user {loginRequest.UserName}");
            var user = await _userRepository.TryLoginAsync(loginRequest.UserName, loginRequest.Password);
            if (user is null)
            {
                _logger.LogWarning("Bad username or password!");
                return Unauthorized("Bad username or password");
            }

            var token = _jwtService.GetJwtToken(user.Id, user.Role);
            _logger.LogInformation("Login successful");
            return Ok(new LoginResponse { UserName = loginRequest.UserName, Token = token });
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest)
        {
            _logger.LogInformation($"Registering user {registerRequest.UserName}");
            if (await _userRepository.ExistAsync(u => u.UserName == registerRequest.UserName))
            {
                _logger.LogWarning("User already exists!");
                return BadRequest("User already exists");
            }

            _passwordService.CreatePasswordHash(registerRequest.Password, out var passwordHash, out var passwordSalt);

            var user = _mapper.Map<User>((registerRequest, passwordHash, passwordSalt));
            var id = await _userRepository.CreateAsync(user);
            _logger.LogInformation($"Created user with ID {id}");
            return Created(nameof(Login), new { id = id });
        }
    }
}

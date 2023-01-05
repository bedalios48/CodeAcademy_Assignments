using AutoMapper;
using GenealogyTree.Domain.Interfaces;
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

        public UserController(IUserRepository userRepository, IJwtService jwtService, IPasswordService passwordService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            var isOk = await _userRepository.TryLoginAsync(loginRequest.UserName, loginRequest.Password, out var user);
            if (!isOk)
                return Unauthorized("Bad username or password");

            var token = _jwtService.GetJwtToken(user.Id, user.Role);


            return Ok(new LoginResponse { UserName = loginRequest.UserName, Token = token });
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest)
        {
            //if (await _userRepository.ExistsAsync(registerRequest.UserName))
            //    return BadRequest("User already exists");

            _passwordService.CreatePasswordHash(registerRequest.Password, out var passwordHash, out var passwordSalt);

            var user = _mapper.Map<User>(registerRequest);

            /*
            var user = new User
            {
                UserName = registerRequest.UserName,
                Role = registerRequest.Role,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            */
            var id = await _userRepository.Register(user);

            return Created(nameof(Login), new { id = id });
        }
    }
}

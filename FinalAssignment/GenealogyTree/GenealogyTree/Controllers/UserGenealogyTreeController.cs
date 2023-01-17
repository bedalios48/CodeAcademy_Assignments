using AutoMapper;
using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using NLog;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace GenealogyTree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserGenealogyTreeController : ControllerBase
    {
        private readonly IRelativeService _relativeService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserGenealogyTreeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _repo;

        public UserGenealogyTreeController(IMapper mapper,
            IRelativeService relativeService,
            ILogger<UserGenealogyTreeController> logger,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork repo)
        {
            _mapper = mapper;
            _relativeService = relativeService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _repo = repo;
        }

        /// <summary>
        /// Fetches all user relatives
        /// </summary>
        /// <returns>All user relatives</returns>
        [HttpGet("/api/user/relatives")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RelativeResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCloseRelatives(int personId)
        {
            _logger.LogInformation($"Getting relative information for person ID {personId}");
            if (!await _repo.Person.ExistAsync(p => p.Id == personId))
            {
                _logger.LogWarning("Person does not exist!");
                return BadRequest("Person does not exist!");
            }

            var mainRelative = await _relativeService.GetMainRelativeAsync(personId);
            if (mainRelative.Relatives.Count() == 0)
            {
                _logger.LogWarning("Person has no relatives!");
                return NoContent();
            }

            _logger.LogInformation($"{mainRelative.Relatives.Count} relatives found");
            var relativeResponses = mainRelative.Relatives.Select(r => _mapper.Map<RelativeResponse>(r));
            return Ok(relativeResponses);
        }

        /// <summary>
        /// Gets user person
        /// </summary>
        /// <returns>Person</returns>
        [HttpGet("/api/user/{key}/person")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUserPerson(int key)
        {
            if (!VerifyUser(key))
                return Forbid();

            _logger.LogInformation($"Getting person assigned to user ID {key}");
            var person = await _repo.Person.GetAsync(p => p.UserId == key);
            if (person is null)
            {
                _logger.LogWarning("Person not found!");
                return NotFound();
            }

            var personResponse = _mapper.Map<PersonResponse>(person);
            _logger.LogInformation($"Found person {JsonConvert.SerializeObject(personResponse)}");
            return Ok(personResponse);
        }

        /// <summary>
        /// Gets person
        /// </summary>
        /// <returns>Person</returns>
        [HttpGet("/api/user/person")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetPerson(int personId)
        {
            _logger.LogInformation($"Getting person with ID {personId}");
            var person = await _repo.Person.GetAsync(p => p.Id == personId);
            if (person is null)
            {
                _logger.LogWarning("Person not found");
                return NotFound();
            }

            var personResponse = _mapper.Map<PersonResponse>(person);
            _logger.LogInformation($"Found person {JsonConvert.SerializeObject(personResponse)}");
            return Ok(personResponse);
        }

        /// <summary>
        /// Finds people
        /// </summary>
        /// <returns>People</returns>
        [HttpGet("/api/user/findPeople")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> FindPeople([FromQuery]FindPersonRequest findPersonRequest)
        {
            _logger.LogInformation($"Looking for people with parameters {JsonConvert.SerializeObject(findPersonRequest)}");
            var people = await _repo.Person.GetAllAsync();
            var filteredPeople = GetFilteredPeople(people, findPersonRequest);

            if (filteredPeople.Count() == 0)
            {
                _logger.LogWarning("No persons found!");
                return NotFound();
            }

            _logger.LogInformation($"{filteredPeople.Count} people found");
            var peopleResponse = filteredPeople.Select(p => _mapper.Map<PersonResponse>(p));
            return Ok(peopleResponse);
        }

        /// <summary>
        /// Create new person
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/user/{key}/createPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreatePerson(int key, [FromBody] CreatePersonRequest createPersonRequest)
        {
            if (!VerifyUser(key))
                return Forbid();

            if(string.IsNullOrEmpty(createPersonRequest.Name) || string.IsNullOrEmpty(createPersonRequest.Surname))
            {
                var message = "Name and surname are mandatory!";
                _logger.LogWarning(message);
                return BadRequest(message);
            }

            _logger.LogInformation($"Creating person {JsonConvert.SerializeObject(createPersonRequest)}");
            var person = _mapper.Map<Person>((createPersonRequest, key));
            if (await _repo.Person.ExistAsync(p => p.Name == person.Name
            && p.Surname == person.Surname
            && p.DateOfBirth == person.DateOfBirth
            && p.BirthPlace == person.BirthPlace))
            {
                _logger.LogWarning("Person already exists!");
                return Conflict("Person already exists!");
            }

            var personId = await _repo.Person.CreateAsync(person);
            _logger.LogInformation($"Created person with ID {personId}");
            return Created("PostPerson", new { PersonId = personId });
        }

        /// <summary>
        /// Create new relative
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/user/{key}/createRelation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateRelation(int key, RelationRequest relativeRequest)
        {
            if (!VerifyUser(key))
                return Forbid();

            if(relativeRequest.ParentId == relativeRequest.ChildId)
            {
                var message = "Parent and child cannot be the same person!";
                _logger.LogWarning(message);
                return BadRequest(message);
            }

            _logger.LogInformation($"Creating relation {JsonConvert.SerializeObject(relativeRequest)}");
            var parentChild = _mapper.Map<ParentChild>((relativeRequest, key));
            if (await _repo.ParentChild.ExistAsync(pc => pc.ParentId == parentChild.ParentId
            && pc.ChildId == parentChild.ChildId))
            {
                _logger.LogWarning("Relation already exists!");
                return BadRequest("Relation already exists!");
            }

            var parentChildId = await _repo.ParentChild.CreateAsync(parentChild);
            _logger.LogInformation($"Created relation with ID {parentChildId}");
            return Created("RelationPost", new { RelationId = parentChildId });
        }

        /// <summary>
        /// Link user to person
        /// </summary>
        /// <returns></returns>
        [HttpPut("/api/user/{key}/linkUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> LinkUser(int key, int personId)
        {
            if (!VerifyUser(key))
                return Forbid();

            _logger.LogInformation($"Creating link between user ID {key} and person ID {personId}");
            var person = await _repo.Person.GetAsync(p => p.Id == personId);
            if(person is null)
            {
                _logger.LogWarning("Person does not exist!");
                return BadRequest("Person does not exist!");
            }

            if(person.UserId is not null)
            {
                _logger.LogWarning("Person already has a user!");
                return BadRequest("Person already has a user!");
            }

            var user = await _repo.User.GetAsync(u => u.Id == key);
            if(user is null)
            {
                _logger.LogWarning("User does not exist!");
                return BadRequest("User does not exist!");
            }

            var people = await _repo.Person.GetAllAsync(p => p.UserId == key);
            if(people.Count() > 0)
            {
                _logger.LogWarning("User already linked to a person!");
                return BadRequest("User already linked to a person!");
            }

            person.UserId = key;
            await _repo.Person.UpdateAsync(person);
            _logger.LogInformation("Person linked to the user");
            return Ok();
        }

        /// <summary>
        /// Add new marriage
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/user/{key}/addMarriage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddMarriage(int key, MarriageRequest marriageRequest)
        {
            if (!VerifyUser(key))
                return Forbid();

            if (marriageRequest.PersonId == marriageRequest.SpouseId)
            {
                var message = "Marriage cannot be between the same person!";
                _logger.LogWarning(message);
                return BadRequest(message);
            }

            if((!await _repo.Person.ExistAsync(p => p.Id == marriageRequest.PersonId)) ||
                (!await _repo.Person.ExistAsync(p => p.Id == marriageRequest.SpouseId)))
            {
                var message = "Persons do not exist!";
                _logger.LogWarning(message);
                return BadRequest(message);
            }

            _logger.LogInformation($"Creating marriage {JsonConvert.SerializeObject(marriageRequest)}");
            var spouses = _mapper.Map<Marriage>((marriageRequest, key));
            if (await _repo.Marriage.ExistAsync(s => (s.PersonId == spouses.PersonId
            && s.SpouseId == spouses.SpouseId) || (s.PersonId == spouses.SpouseId && s.SpouseId == spouses.PersonId)))
            {
                var message = "Marriage already recorded!";
                _logger.LogWarning(message);
                return BadRequest(message);
            }

            var spousesId = await _repo.Marriage.CreateAsync(spouses);
            _logger.LogInformation($"Created marriage with ID {spousesId}");
            return Created("MarriagePost", new { MarriageId = spousesId });
        }

        private bool VerifyUser(int key)
        {
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserId != key)
            {
                _logger.LogWarning($"User {currentUserId} tried to act as user {key}");
                return false;
            }

            return true;
        }

        private List<Person> GetFilteredPeople(List<Person> people, FindPersonRequest findPerson)
        {
            if (!string.IsNullOrEmpty(findPerson.Name))
                people = people.Where(p => p.Name == findPerson.Name).ToList();

            if (!string.IsNullOrEmpty(findPerson.Surname))
                people = people.Where(p => p.Surname == findPerson.Surname).ToList();

            if (DateTime.TryParse(findPerson.DateOfBirth, out var date))
                people = people.Where(p => p.DateOfBirth == date).ToList();

            if (!string.IsNullOrEmpty(findPerson.BirthPlace))
                people = people.Where(p => p.BirthPlace == findPerson.BirthPlace).ToList();

            return people;
        }
    }
}

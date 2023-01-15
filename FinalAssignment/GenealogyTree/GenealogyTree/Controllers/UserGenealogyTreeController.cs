using AutoMapper;
using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net.Mime;

namespace GenealogyTree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGenealogyTreeController : ControllerBase
    {
        private readonly IRelativeService _relativeService;
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepo;
        private readonly IParentChildRepository _parentChildRepo;

        public UserGenealogyTreeController(IMapper mapper, IRelativeService relativeService
            , IPersonRepository personRepo, IParentChildRepository parentChildRepo)
        {
            _mapper = mapper;
            _relativeService = relativeService;
            _personRepo = personRepo;
            _parentChildRepo = parentChildRepo;
        }

        /// <summary>
        /// Fetches all user relatives
        /// </summary>
        /// <returns>All user relatives</returns>
        [HttpGet("/api/user/relatives")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RelativeResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCloseRelatives(int personId)
        {
            // check if user exists
            var mainRelative = await _relativeService.GetMainRelative(personId);
            // check if user has relatives
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
            var person = await _personRepo.GetAsync(p => p.UserId == key);
            if (person is null)
                return NotFound();

            var personResponse = _mapper.Map<PersonResponse>(person);
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
            var person = await _personRepo.GetAsync(p => p.Id == personId);
            if (person is null)
                return NotFound();

            var personResponse = _mapper.Map<PersonResponse>(person);
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
            var people = await _personRepo.GetAllAsync();
            var filteredPeople = GetFilteredPeople(people, findPersonRequest);

            if (filteredPeople.Count() == 0)
                return NotFound();

            var peopleResponse = filteredPeople.Select(p => _mapper.Map<PersonResponse>(p));
            return Ok(peopleResponse);
        }

        private List<Person> GetFilteredPeople(List<Person> people, FindPersonRequest findPerson)
        {
            if (!string.IsNullOrEmpty(findPerson.Name))
                people = people.Where(p => p.Name == findPerson.Name).ToList();

            if (!string.IsNullOrEmpty(findPerson.Surname))
                people = people.Where(p => p.Surname == findPerson.Surname).ToList();

            if (findPerson.DateOfBirth is not null)
                people = people.Where(p => p.DateOfBirth == findPerson.DateOfBirth).ToList();

            if (!string.IsNullOrEmpty(findPerson.BirthPlace))
                people = people.Where(p => p.BirthPlace == findPerson.BirthPlace).ToList();

            return people;
        }

        /// <summary>
        /// Create new person
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/user/{key}/createPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreatePerson(int key, CreatePersonRequest createPersonRequest)
        {
            var person = _mapper.Map<Person>((createPersonRequest, key));
            if (await _personRepo.ExistAsync(p => p.Name == person.Name
            && p.Surname == person.Surname
            && p.DateOfBirth == person.DateOfBirth
            && p.BirthPlace == person.BirthPlace))
                return BadRequest("Person already exists!");

            var personId = await _personRepo.CreateAsync(person);

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
            var parentChild = _mapper.Map<ParentChild>((relativeRequest, key));
            if (await _parentChildRepo.ExistAsync(pc => pc.ParentId == parentChild.ParentId
            && pc.ChildId == parentChild.ChildId))
                return BadRequest("Relation already exists!");

            var parentChildId = await _parentChildRepo.CreateAsync(parentChild);
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
            var person = await _personRepo.GetAsync(p => p.Id == personId);
            person.UserId = key;

            await _personRepo.UpdateAsync(person);
            return Ok();
        }
    }
}

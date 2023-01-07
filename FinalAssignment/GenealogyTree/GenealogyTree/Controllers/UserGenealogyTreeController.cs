using AutoMapper;
using GenealogyTree.Domain.Interfaces;
using GenealogyTree.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace GenealogyTree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGenealogyTreeController : ControllerBase
    {
        private readonly IRelativeService _relativeService;
        private readonly IMapper _mapper;

        public UserGenealogyTreeController(IMapper mapper, IRelativeService relativeService)
        {
            _mapper = mapper;
            _relativeService = relativeService;
        }

        /// <summary>
        /// Fetches all user relatives
        /// </summary>
        /// <returns>All user relatives</returns>
        [HttpGet("/api/user/{key}/relatives")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RelativeResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllRelatives(int key)
        {
            // check if user exists
            var mainRelative = await _relativeService.GetMainRelative(key);
            // check if user has relatives
            var relativeResponses = mainRelative.Relatives.Select(r => _mapper.Map<RelativeResponse>(r));
            return Ok(relativeResponses);
        }
    }
}

using BugTicketingSystem.BL.Dtos.BugDtos;
using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Managers.BugManager;
using BugTicketingSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BugTicketingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBugManager _bugManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public BugsController(IConfiguration configuration,
            IBugManager bugManager,
            IStringLocalizer<SharedResources> localizer)
        {
            _configuration = configuration;
            _bugManager = bugManager;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<Ok<List<BugReadDto>>>
            GetAllProjects()
        {
            var projects = await _bugManager.GetAllBugsAsync();
            return TypedResults.Ok(projects);
        }
        [HttpGet("{id}")]
        public async Task<Results<Ok<BugReadDto>, NotFound>>
            GetProjectById(Guid id)
        {
            var project = await _bugManager.GetBugByIdAsync(id);
            if (project == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult>>
            Add([FromBody] BugAddDto bugAddDto)
        {
            var result = await _bugManager.AddBugAsync(bugAddDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

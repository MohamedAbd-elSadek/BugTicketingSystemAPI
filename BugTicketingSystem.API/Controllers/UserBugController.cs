using BugTicketingSystem.BL.Managers.BugUserManager;
using BugTicketingSystem.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BugTicketingSystem.API.Controllers
{
    [Route("api/bugs/{bugId}/assignees")]
    [ApiController]
    public class UserBugController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBugUserManager _bugUserManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UserBugController(
            IConfiguration configuration,
            IBugUserManager bugUserManager,
            IStringLocalizer<SharedResources> localizer)
        {
            _configuration = configuration;
            _bugUserManager = bugUserManager;
            _localizer = localizer;

        }
        [HttpPost]
        public async Task<IActionResult> AssignUserToBug([FromQuery] Guid bugId, [FromQuery] Guid userId)
        {
            try
            {
                await _bugUserManager.AssignUserToBugAsync(bugId, userId);
                return Ok(new { message = "User assigned to bug successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> UnAssignUserToBug([FromRoute] Guid bugId, [FromRoute] Guid userId)
        {
            try
            {
                await _bugUserManager.UnAssignUserToBugAsync(bugId, userId);
                return Ok(new { message = "User unassigned from bug successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

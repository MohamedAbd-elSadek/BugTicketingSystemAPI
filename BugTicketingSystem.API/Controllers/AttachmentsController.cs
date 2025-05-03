using BugTicketingSystem.BL.Dtos.AttachmentDtos;
using BugTicketingSystem.BL.Managers.AttachmentManager;
using BugTicketingSystem.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BugTicketingSystem.API.Controllers
{
    [Route("api/bugs/{bugId}/attachments")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAttachmentManager _attachmentManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public AttachmentsController(IConfiguration configuration,
            IAttachmentManager attachmentManager,
            IStringLocalizer<SharedResources> localizer)
        {
            _configuration = configuration;
            _attachmentManager = attachmentManager;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttachmentsByBugIdAsync(Guid bugId)
        {
            try
            {
                var attachments = await _attachmentManager.GetAttachmentsByBugIdAsync(bugId);
                return Ok(attachments);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAttachmentToBugAsync(
                Guid bugId,
                IFormFile file
            )
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(new { message = "No file uploaded." });

                // Generate unique filename
                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

                // Save to server
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Attachments");
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Generate accessible URL
                var fileUrl = $"{Request.Scheme}://{Request.Host}/attachments/{uniqueFileName}";

                // Save to database
                var result = await _attachmentManager.AddAttachmentToBugAsync(
                    bugId,
                    new AttachmentAddDto
                    {
                        FileName = file.FileName,
                        FilePath = fileUrl
                    }
                );

                // Return URL in response
                return Ok(new
                {
                    IsSuccess = true,
                    FileUrl = fileUrl,
                    FileName = file.FileName
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{attachmentId}")]
        public async Task<IActionResult> DeleteAttachmentFromBugAsync(
            Guid bugId,
            Guid attachmentId)
        {
            try
            {
                // Verify attachment belongs to bug
                var attachment = await _attachmentManager.GetAttachmentByIdAsync(attachmentId);
                if (attachment == null || !await _attachmentManager.AttachmentBelongsToBug(attachmentId, bugId))
                {
                    return NotFound(new { message = "Attachment not found for this bug" });
                }

                await _attachmentManager.DeleteAttachmentAsync(attachmentId);
                return Ok(new { message = "Attachment deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
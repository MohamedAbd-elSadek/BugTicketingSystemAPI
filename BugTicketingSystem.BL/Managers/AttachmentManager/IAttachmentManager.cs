using BugTicketingSystem.BL.Dtos.AttachmentDtos;
using BugTicketingSystem.BL.Dtos.Common;
using Microsoft.AspNetCore.Http;

namespace BugTicketingSystem.BL.Managers.AttachmentManager
{
    public interface IAttachmentManager
    {
        Task<List<AttachmentReadDto>> GetAllAttachmentsAsync();
        Task<AttachmentReadDto> GetAttachmentByIdAsync(Guid id);
        Task<GeneralResult> AddAttachmentAsync(AttachmentAddDto attachmentAddDto);
        Task UpdateAttachmentAsync(AttachmentUpdateDto attachmentUpdateDto);
        Task DeleteAttachmentAsync(Guid id);
        Task<List<AttachmentReadDto>> GetAttachmentsByBugIdAsync(Guid bugId);
        Task<GeneralResult> AddAttachmentToBugAsync(Guid bugId, AttachmentAddDto attachmentAddDto);

        Task<bool> AttachmentBelongsToBug(Guid attachmentId, Guid bugId);
        //Task<AttachmentAddDto> UploadeAttachmentAsync(Guid bugId,IFormFile file);
    }
}
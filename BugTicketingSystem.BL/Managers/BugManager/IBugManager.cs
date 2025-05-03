using BugTicketingSystem.BL.Dtos.BugDtos;
using BugTicketingSystem.BL.Dtos.Common;

namespace BugTicketingSystem.BL.Managers.BugManager
{
    public interface IBugManager
    {
        Task<List<BugReadDto>> GetAllBugsAsync();
        Task<BugReadDto> GetBugByIdAsync(Guid id);
        Task<GeneralResult> AddBugAsync(BugAddDto bugAddDto);
        Task UpdateBugAsync(BugUpdateDto bugUpdateDto);
        Task DeleteBugAsync(Guid id);
    }
}

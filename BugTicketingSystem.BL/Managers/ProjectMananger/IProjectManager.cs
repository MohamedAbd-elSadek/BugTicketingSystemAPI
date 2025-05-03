using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Dtos.ProjectDtos;

namespace BugTicketingSystem.BL.Managers.ProjectMananger
{
    public interface IProjectManager
    {
        Task<List<ProjectReadDto>> GetAllProjectsAsync();
        Task<ProjectReadDto> GetProjectByIdAsync(Guid id);
        Task<GeneralResult> AddProjectAsync(ProjectAddDto projectAddDto);
        Task UpdateProjectAsync(ProjectUpdateDto projectUpdateDto);
        Task DeleteProjectAsync(Guid id);
    }
}
using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Dtos.ProjectDtos;
using BugTicketingSystem.BL.Validators.ProjectValidators;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.UnitOfWork;
using FluentValidation;

namespace BugTicketingSystem.BL.Managers.ProjectMananger
{
    public class ProjectMananger : IProjectManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProjectAddValidator _projectAddValidator;
        private readonly ProjectUpdateValidator _projectUpdateValidator;

        public ProjectMananger(IUnitOfWork unitOfWork,
            ProjectAddValidator projectAddValidator,
            ProjectUpdateValidator projectUpdateValidator)
        {
            _unitOfWork = unitOfWork;
            _projectAddValidator = projectAddValidator;
            _projectUpdateValidator = projectUpdateValidator;
        }
        public async Task<List<ProjectReadDto>> GetAllProjectsAsync()
        {
            var projects = await _unitOfWork.ProjectRepo.GetAllAsync();
            return projects.Select(x => new ProjectReadDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();

        }
        public async Task<ProjectReadDto> GetProjectByIdAsync(Guid id)
        {
            var prject = await _unitOfWork.ProjectRepo.GetByIdAsync(id);
            if (prject == null)
            {
                throw new Exception("project Not Found");
            }
            return new ProjectReadDto
            {
                Id = prject.Id,
                Name = prject.Name,
                Description = prject.Description,
            };

            
        }
        public async Task<GeneralResult> AddProjectAsync(ProjectAddDto projectAddDto)
        {
            var validationResult = await _projectAddValidator.ValidateAsync(projectAddDto);
            if(!validationResult.IsValid)
            {
                return new GeneralResult
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors
                    .Select(e => new ResultError
                    {
                        Code = e.ErrorCode,
                        Message = e.ErrorMessage
                    }).ToArray()
                };
            }
            var project = new Project
            {
                Name = projectAddDto.Name,
                Description = projectAddDto.Description,
                //CreatedById = createdById,
            };
            _unitOfWork.ProjectRepo.Add(project);
            await _unitOfWork.SaveChangesAsync();
            return new GeneralResult
            {
                IsSuccess = true,
                Errors = []
            };

        }
        public async Task DeleteProjectAsync(Guid id)
        {

            var project = await _unitOfWork.ProjectRepo.GetByIdAsync(id);
            if (project == null)
            {
                throw new Exception("Project Not Found");
            }
            _unitOfWork.ProjectRepo.Delete(project);
            await _unitOfWork.SaveChangesAsync();
            
        }
        public async Task UpdateProjectAsync(ProjectUpdateDto projectUpdateDto)
        {
            var validationResult = await _projectUpdateValidator.ValidateAsync(projectUpdateDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var project = await _unitOfWork.ProjectRepo.GetByIdAsync(projectUpdateDto.Id);
            if (project == null)
            {
                throw new Exception("Project Not Found");
            }
            project.Name = projectUpdateDto.Name;
            project.Description = projectUpdateDto.Description;
            _unitOfWork.ProjectRepo.Update(project);
            await _unitOfWork.SaveChangesAsync();


        }
    }
}

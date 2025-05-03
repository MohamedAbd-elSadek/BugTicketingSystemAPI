using BugTicketingSystem.BL.Dtos.BugDtos;
using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Dtos.UserDtos;
using BugTicketingSystem.BL.Validators.BugValidators;
using BugTicketingSystem.BL.Validators.UserValidators;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.UnitOfWork;
using FluentValidation;

namespace BugTicketingSystem.BL.Managers.BugManager
{
    public class BugManager : IBugManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BugAddValidator _bugAddValidator;
        private readonly BugUpdateValidator _bugUpdateValidator;
        public BugManager(IUnitOfWork unitOfWork, BugUpdateValidator bugUpdateValidator,
            BugAddValidator bugAddValidator)
        {
            _unitOfWork = unitOfWork;
            _bugUpdateValidator = bugUpdateValidator;
            _bugAddValidator = bugAddValidator;
        }

        public async Task<List<BugReadDto>> GetAllBugsAsync()
        {
            var bugs = await _unitOfWork.BugRepo.GetAllAsync();
            return bugs.Select(b => new BugReadDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Status = b.Status
            }).ToList();
        }

        public async Task<BugReadDto> GetBugByIdAsync(Guid id)
        {
            var bug = await _unitOfWork.BugRepo.GetByIdAsync(id);
            if (bug == null)
            {
                throw new Exception("Bug not found");
            }
            return new BugReadDto
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                Status = bug.Status
            };

        }
        public async Task<GeneralResult> AddBugAsync(BugAddDto bugAddDto)
        {
            var validationResult = await _bugAddValidator.ValidateAsync(bugAddDto);
            if (!validationResult.IsValid)
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
            // Convert string status to enum
            var bugStatus = Enum.Parse<BugStatus>(bugAddDto.Status, true);
            var bug = new Bug
            {
                Title = bugAddDto.Title,
                Description = bugAddDto.Description,
                Status = bugStatus,
                ProjectId = bugAddDto.ProjectId,
            };
            _unitOfWork.BugRepo.Add(bug);
            await _unitOfWork.SaveChangesAsync();
            return new GeneralResult
            {
                IsSuccess = true,
                Errors = []
            };

        }

        public async Task DeleteBugAsync(Guid id)
        {
            var bug = await _unitOfWork.BugRepo.GetByIdAsync(id);
            if (bug == null)
            {
                throw new Exception("Bug not found");
            }
            _unitOfWork.BugRepo.Delete(bug);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task UpdateBugAsync(BugUpdateDto bugUpdateDto)
        {
            var validationResult = await _bugUpdateValidator.ValidateAsync(bugUpdateDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var existingBug = await _unitOfWork.BugRepo.GetByIdAsync(bugUpdateDto.Id);
            if (existingBug == null)
            {
                throw new Exception("Bug not found");
            }
            existingBug.Title = bugUpdateDto.Title;
            existingBug.Description = bugUpdateDto.Description;
            existingBug.Status = bugUpdateDto.Status;
            _unitOfWork.BugRepo.Update(existingBug);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}

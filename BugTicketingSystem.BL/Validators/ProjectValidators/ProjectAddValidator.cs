using BugTicketingSystem.BL.Constants;
using BugTicketingSystem.BL.Dtos.ProjectDtos;
using BugTicketingSystem.DAL.UnitOfWork;
using BugTicketingSystem.Shared;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BugTicketingSystem.BL.Validators.ProjectValidators
{
    public class ProjectAddValidator: AbstractValidator<ProjectAddDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ProjectAddValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;

            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.NameRequired])
                .WithErrorCode(ErrorConstants.ErrorCodes.NameRequired)
                .MinimumLength(3)
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.NameCannotBeShorterThan3Characters])
                .WithErrorCode(ErrorConstants.ErrorCodes.NameCannotBeShorterThan3Characters)
                .MustAsync(CheckProjectNameIsUnique)
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.ProjectNameMustBeUnique])
                .WithErrorCode(ErrorConstants.ErrorCodes.ProjectNameMustBeUnique);
        }
        private async Task<bool> CheckProjectNameIsUnique(
            string projectName,
            CancellationToken cancellationToken)
        {
            var projects = await _unitOfWork.ProjectRepo.GetAllAsync();
            return !projects.Any(p => p.Name == projectName);
        }
    }
}

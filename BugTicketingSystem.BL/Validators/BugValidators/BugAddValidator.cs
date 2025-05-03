using BugTicketingSystem.BL.Constants;
using BugTicketingSystem.BL.Dtos.BugDtos;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.UnitOfWork;
using BugTicketingSystem.Shared;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BugTicketingSystem.BL.Validators.BugValidators
{
    public class BugAddValidator: AbstractValidator<BugAddDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public BugAddValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;

            RuleFor(b => b.Title)
                .NotEmpty()
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.NameRequired])
                .WithErrorCode(ErrorConstants.ErrorCodes.NameRequired)

                .MinimumLength(3)
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.NameCannotBeShorterThan3Characters])
                .WithErrorCode(ErrorConstants.ErrorCodes.NameCannotBeShorterThan3Characters);

            RuleFor(b => b.Status)
                .NotEmpty()
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.BugStatusRequired])
                .WithErrorCode(ErrorConstants.ErrorCodes.BugStatusRequired)
                .Must(s => Enum.TryParse<BugStatus>(s, true, out _))
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.InvalidBugStatus])
                .WithErrorCode(ErrorConstants.ErrorCodes.InvalidBugStatus);

        }
    }
}

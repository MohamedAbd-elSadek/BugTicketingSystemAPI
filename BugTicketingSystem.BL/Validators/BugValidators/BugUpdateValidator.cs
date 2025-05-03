using BugTicketingSystem.BL.Constants;
using BugTicketingSystem.BL.Dtos.BugDtos;
using BugTicketingSystem.DAL.UnitOfWork;
using BugTicketingSystem.Shared;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BugTicketingSystem.BL.Validators.BugValidators
{
    public class BugUpdateValidator : AbstractValidator<BugUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public BugUpdateValidator(
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

        }
    }
}

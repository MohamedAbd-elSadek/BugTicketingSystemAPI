using BugTicketingSystem.BL.Constants;
using BugTicketingSystem.BL.Dtos.AttachmentDtos;
using BugTicketingSystem.DAL.UnitOfWork;
using BugTicketingSystem.Shared;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BugTicketingSystem.BL.Validators.AttachmentValidators
{

    public class AttachmentAddValidator: AbstractValidator<AttachmentAddDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public AttachmentAddValidator(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;

            RuleFor(a => a.FileName)
                .NotEmpty()
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.NameRequired])
                .WithErrorCode(ErrorConstants.ErrorCodes.NameRequired)
                .MinimumLength(3)
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.NameCannotBeShorterThan3Characters])
                .WithErrorCode(ErrorConstants.ErrorCodes.NameCannotBeShorterThan3Characters)
                
                .MustAsync(CheckFileNameIsUnique)
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.FileNameMustBeUnique])
                .WithErrorCode(ErrorConstants.ErrorCodes.FileNameMustBeUnique);


            RuleFor(a => a.FilePath)
                .NotEmpty()
                .WithMessage(_localizer[ErrorConstants.ErrorMessages.FilePathRequired])
                .WithErrorCode(ErrorConstants.ErrorCodes.FilePathRequired);


        }
        private async Task<bool> CheckFileNameIsUnique(
            string fileName,
            CancellationToken cancellationToken)
        {
            var attachments = await _unitOfWork.AttachmentRepo.GetAllAsync();
            return !attachments.Any(a => a.FileName == fileName);
        }

    }
}

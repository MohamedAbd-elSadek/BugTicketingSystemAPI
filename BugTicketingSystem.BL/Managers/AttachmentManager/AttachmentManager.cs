using BugTicketingSystem.BL.Dtos.AttachmentDtos;
using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Validators.AttachmentValidators;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BugTicketingSystem.BL.Managers.AttachmentManager
{
    public class AttachmentManager : IAttachmentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AttachmentAddValidator _attachmentAddValidator;
        private readonly AttachmentUpdateValidator _attachmentUpdateValidator;

        public AttachmentManager(
            IUnitOfWork unitOfWork,
            AttachmentUpdateValidator attachmentUpdateValidator,
            AttachmentAddValidator attachmentAddValidator
            )
        {
            _unitOfWork = unitOfWork;
            _attachmentUpdateValidator = attachmentUpdateValidator;
            _attachmentAddValidator = attachmentAddValidator;
        }
        public async Task<List<AttachmentReadDto>> GetAllAttachmentsAsync()
        {
            var attachments = await _unitOfWork.AttachmentRepo.GetAllAsync();
            return attachments.Select(a => new AttachmentReadDto
            {
                Id = a.Id,
                FileName = a.FileName,
            }).ToList();
        }

        public async Task<AttachmentReadDto> GetAttachmentByIdAsync(Guid id)
        {
            var attachment = await _unitOfWork.AttachmentRepo.GetByIdAsync(id);
            if (attachment == null)
            {
                throw new Exception("Attachment not found");
            }
            return new AttachmentReadDto
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
            };

        }
        public async Task<GeneralResult> AddAttachmentAsync(AttachmentAddDto attachmentAddDto)
        {
            var validationResult = await _attachmentAddValidator.ValidateAsync(attachmentAddDto);
            if (!validationResult.IsValid)
            {
                // Result Pattern
                return new GeneralResult
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors
                    .Select(e => new ResultError
                    {
                        Code = e.ErrorCode,
                        Message = e.ErrorMessage
                    }).ToArray(),
                };
            }
            var attachment = new Attachment
            {
                FileName = attachmentAddDto.FileName,
                FilePath = attachmentAddDto.FilePath,
            };
            _unitOfWork.AttachmentRepo.Add(attachment);
            await _unitOfWork.SaveChangesAsync();
            return new GeneralResult
            {
                IsSuccess = true,
                Errors = [],
            };

        }

        public async Task DeleteAttachmentAsync(Guid id)
        {
            var attachment = await _unitOfWork.AttachmentRepo.GetByIdAsync(id);
            if (attachment == null)
            {
                throw new Exception("Attachment not found");
            }
            _unitOfWork.AttachmentRepo.Delete(attachment);
            await _unitOfWork.SaveChangesAsync();

        }


        public async Task UpdateAttachmentAsync(AttachmentUpdateDto attachmentUpdateDto)
        {
            var validationResult = await _attachmentUpdateValidator.ValidateAsync(attachmentUpdateDto);
            if (!validationResult.IsValid)
            {
                
                throw new ValidationException(validationResult.Errors);
            }
            var attachment = await _unitOfWork.AttachmentRepo.GetByIdAsync(attachmentUpdateDto.Id);
            if (attachment == null)
            {
                throw new Exception("Attachment not found");
            }
            attachment.FileName = attachmentUpdateDto.FileName;
            attachment.FilePath = attachmentUpdateDto.FilePath;
            _unitOfWork.AttachmentRepo.Update(attachment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<AttachmentReadDto>> GetAttachmentsByBugIdAsync(Guid bugId)
        {
            var attachments = await _unitOfWork.AttachmentRepo.GetAttachmentsByBugIdAsync(bugId);
            return attachments.Select(a => new AttachmentReadDto
            {
                Id = a.Id,
                FileName = a.FileName,
                FilePath = a.FilePath
            }).ToList();
        }

        public async Task<bool> AttachmentBelongsToBug(Guid attachmentId, Guid bugId)
        {
            var attachment = await _unitOfWork.AttachmentRepo.GetByIdAsync(attachmentId);
            return attachment?.BugId == bugId;
        }
        public async Task<GeneralResult> AddAttachmentToBugAsync(
    Guid bugId,
    AttachmentAddDto attachmentAddDto
)
        {
            var validationResult = await _attachmentAddValidator.ValidateAsync(attachmentAddDto);
            if (!validationResult.IsValid)
            {
                return new GeneralResult
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(e => new ResultError
                    {
                        Code = e.ErrorCode,
                        Message = e.ErrorMessage
                    }).ToArray()
                };
            }

            var attachment = new Attachment
            {
                FileName = attachmentAddDto.FileName,
                FilePath = attachmentAddDto.FilePath, 
                BugId = bugId
            };

            _unitOfWork.AttachmentRepo.Add(attachment);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                IsSuccess = true,
                Errors = []
            };
        }
    }
}

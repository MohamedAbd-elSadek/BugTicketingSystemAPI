using FluentValidation.Results;

namespace BugTicketingSystem.BL.Exceptions
{
    public class BusinessValidationExceptions: Exception
    {
        public List<ValidationFailure> Errors { get; }
        public BusinessValidationExceptions( List<ValidationFailure> errors)
        {
            Errors = errors;
        }
    }
}

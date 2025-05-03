
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.UnitOfWork;

namespace BugTicketingSystem.BL.Managers.BugUserManager
{
    public class BugUserManager : IBugUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public BugUserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AssignUserToBugAsync( Guid bugId, Guid userId)
        {
            var bug = await _unitOfWork.BugRepo.GetByIdAsync(bugId);
            var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);
            if (bug == null || user == null) throw new Exception("Invalid IDs.");

            _unitOfWork.BugUserRepo.Add(new BugUser
            {
                BugId = bugId,
                UserId = userId
            });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnAssignUserToBugAsync(Guid bugId, Guid userId)
        {
            var bugUser = await _unitOfWork.BugUserRepo
                .GetByBugIdAndUserIdAsync(bugId, userId); 

            if (bugUser == null)
                throw new Exception("User not assigned to this bug.");

            _unitOfWork.BugUserRepo.Delete(bugUser);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

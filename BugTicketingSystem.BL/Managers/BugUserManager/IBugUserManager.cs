namespace BugTicketingSystem.BL.Managers.BugUserManager
{
    public interface IBugUserManager
    {
         Task AssignUserToBugAsync(Guid bugId, Guid userId);
         Task UnAssignUserToBugAsync(Guid bugId, Guid userId);
         

    }
}
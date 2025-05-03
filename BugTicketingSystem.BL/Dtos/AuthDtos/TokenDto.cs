namespace BugTicketingSystem.BL.Dtos.AuthDtos
{
   public record TokenDto(string Token, DateTime ExpirationDate);
}

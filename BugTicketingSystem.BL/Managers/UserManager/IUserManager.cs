using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Dtos.UserDtos;

namespace BugTicketingSystem.BL.Managers.UserManager
{
    public interface IUserManager
    {
        Task<List<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(Guid id);
        Task<GeneralResult> AddUserAsync(UserAddDto userAddDto);
        Task UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task DeleteUserAsync(Guid id);

    }
}
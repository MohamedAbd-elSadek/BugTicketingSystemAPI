using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Dtos.UserDtos;
using BugTicketingSystem.BL.Validators.UserValidators;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.UnitOfWork;
using FluentValidation;
namespace BugTicketingSystem.BL.Managers.UserManager
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserAddValidator _userAddValidator;
        private readonly UserUpdateValidator _userUpdateValidator;
        public UserManager(IUnitOfWork unitOfWork, 
            UserUpdateValidator userUpdateValidator,
            UserAddValidator userAddValidator)
        {
            _unitOfWork = unitOfWork;
            _userUpdateValidator = userUpdateValidator;
            _userAddValidator = userAddValidator;
        }

        public async Task<List<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            return users.Select(u => new UserReadDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }
        public async Task<UserReadDto> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
        public async Task<GeneralResult> AddUserAsync(UserAddDto userAddDto)
        {
            var validationResult = await _userAddValidator.ValidateAsync(userAddDto);
            if (!validationResult.IsValid)
            {
                return new GeneralResult
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors
                    .Select(e => new ResultError
                    {
                        Code = e.ErrorCode,
                        Message = e.ErrorMessage
                    }).ToArray()
 
                };
            }
            var role = Enum.Parse<UserRole>(userAddDto.Role, true);
            var user = new User
            {
                
                Name = userAddDto.Name,
                Email = userAddDto.Email,
                Role = role,
                PasswordHash = userAddDto.Password

            };
            _unitOfWork.UserRepo.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return new GeneralResult
            {
                IsSuccess = true,
                Errors = []
            };
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            _unitOfWork.UserRepo.Delete(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var validationResult = await _userUpdateValidator.ValidateAsync(userUpdateDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var user = await _unitOfWork.UserRepo.GetByIdAsync(userUpdateDto.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = Enum.Parse<UserRole>(userUpdateDto.Role, true);

            user.Name = userUpdateDto.Name;
            user.Email = userUpdateDto.Email;
            user.Role = role;
            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

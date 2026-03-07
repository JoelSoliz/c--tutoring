using WatchTrackerAPI.DTOs;

namespace WatchTrackerAPI.Interfaces
{
    public interface IUserService
    {
        public UserResponse CreateUser(CreateUserRequest user);
        public UserResponse GetUser(Guid userId);
    }
}

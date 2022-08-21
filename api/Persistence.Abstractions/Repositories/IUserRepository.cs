using Domain;

namespace Persistence;

public interface IUserRepository
{
    Task<List<UserResponse>> GetAllAsync();
    Task<User?> GetAsync(string emailAddress);
}
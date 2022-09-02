using Domain;

namespace Persistence;

public interface IUserRepository
{
    Task<List<UserResponse>> GetAllAsync();
    Task<User?> GetByEmailAddressAsync(string emailAddress);
    Task<User> GetByIdAsync(Guid id);
}
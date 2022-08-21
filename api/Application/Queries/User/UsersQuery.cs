using Domain;
using Persistence;

namespace Application;

public class UsersQuery : IUsersQuery
{
    private readonly IUserRepository _userRepository;
    
    public UsersQuery(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ICollection<UserResponse>> ExecuteAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}
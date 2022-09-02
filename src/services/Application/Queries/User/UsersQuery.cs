using Domain;
using Persistence;

namespace Application;

public class UsersQuery : IUsersQuery
{
    private readonly IRepositoryFactory _repositoryFactory;

    public UsersQuery(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<ICollection<UserResponse>> ExecuteAsync()
    {
        return await _repositoryFactory.UserRepository.GetAllAsync();
    }
}
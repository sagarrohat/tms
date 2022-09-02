using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _dbContext;

    public UserRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAddressAsync(string emailAddress)
    {
        return await _dbContext.Users.Where(x
            => x.EmailAddress == emailAddress && x.IsActive == true).SingleOrDefaultAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.Where(x => x.Id == id && x.IsActive == true).SingleAsync();
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = _dbContext.Users.Where(x => x.IsActive == true).Select(x => new UserResponse()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            EmailAddress = x.EmailAddress
        });

        return await users.ToListAsync();
    }
}
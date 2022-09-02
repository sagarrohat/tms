using Domain;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Persistence;

public class MessagingConfigurationRepository : IMessagingConfigurationRepository
{
    private readonly DatabaseContext _dbContext;

    public MessagingConfigurationRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrUpdateAsync(UserContext userContext, string token)
    {
        var result = await _dbContext.MessagingConfigurations.FirstOrDefaultAsync(x => x.UserId == userContext.UserId);

        if (result != null)
        {
            result.Token = token;
            result.ModifiedOnUtc = DateTime.UtcNow;
        }
        else
        {
            _dbContext.Add(new MessagingConfiguration()
            {
                UserId = userContext.UserId,
                Token = token,
                ModifiedOnUtc = DateTime.UtcNow
            });
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<string?> GetAsync(Guid userId)
    {
        return await _dbContext.MessagingConfigurations
            .Where(x => x.UserId == userId)
            .Select(x => x.Token)
            .FirstOrDefaultAsync();
    }
}
using Common;
using Domain;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application;

public class MessagingConfigurationCommand : IMessagingConfigurationCommand
{
    private readonly IRepositoryFactory _repositoryFactory;

    public MessagingConfigurationCommand(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task ExecuteAsync(UserContext userContext, string token)
    {
        ValidateToken(token);

        await _repositoryFactory.MessagingConfigurationRepository.AddOrUpdateAsync(userContext, token);
    }

    private static void ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            throw new AppException(ErrorCodes.BadRequest, string.Format(ErrorMessages.Required, nameof(token)));
    }
}
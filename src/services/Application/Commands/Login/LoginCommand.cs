using Common;
using Persistence;

namespace Application;

public class LoginCommand : ILoginCommand
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IJwtProvider _jwtService;
    private readonly IHashingService _hashingService;

    public LoginCommand(IRepositoryFactory repositoryFactory, IJwtProvider jwtService, IHashingService hashingService)
    {
        _repositoryFactory = repositoryFactory;
        _jwtService = jwtService;
        _hashingService = hashingService;
    }

    public async Task<JwtResponse> ExecuteAsync(LoginRequest request)
    {
        var user = await _repositoryFactory.UserRepository.GetByEmailAddressAsync(request.EmailAddress);

        if (user is null)
        {
            throw InvalidCredentialsException();
        }

        if (!_hashingService.Verify(request.Pd, user.Pd))
        {
            throw InvalidCredentialsException();
        }

        return _jwtService.GetJwt(new JwtRequest
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
        });
    }

    private static AppException InvalidCredentialsException()
    {
        return new AppException(ErrorCodes.Unauthorized, ErrorMessages.InvalidCredentials);
    }
}
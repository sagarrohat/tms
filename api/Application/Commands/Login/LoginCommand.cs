using Common;
using Persistence;

namespace Application;

public class LoginCommand : ILoginCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtService;
    private readonly IHashingService _hashingService;

    public LoginCommand(IUserRepository userRepository, IJwtProvider jwtService, IHashingService hashingService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _hashingService = hashingService;
    }

    public async Task<JwtResponse> ExecuteAsync(LoginRequest request)
    {
        var user = await _userRepository.GetAsync(request.EmailAddress);

        if (user is null)
        {
            throw new AppException(ErrorCodes.Unauthorized, ErrorMessages.InvalidCredentials);
        }

        if (!_hashingService.Verify(request.Pd, user.Pd))
        {
            throw new AppException(ErrorCodes.Unauthorized, ErrorMessages.InvalidCredentials);
        }
        
        return _jwtService.GetJwt(new JwtRequest
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
        });
    }
}

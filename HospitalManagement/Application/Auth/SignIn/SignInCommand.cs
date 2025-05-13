using HospitalManagement.JwtConfiguration.Services;
using MediatR;

namespace HospitalManagement.Application.Auth.SignIn;

public class SignInCommand(SignInRequestDto request) : IRequest<SignInResponseDto>
{ 
    public SignInRequestDto Request { get; } = request;
}

public class SignInCommandHandler(IAuthService service) : IRequestHandler<SignInCommand, SignInResponseDto>
{
    public Task<SignInResponseDto> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var result = service.GetToken(request.Request.Username);

        return Task.FromResult(new SignInResponseDto()
        {
            AccessToken = result
        });
    }
}
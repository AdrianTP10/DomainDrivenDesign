using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{

    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        //1.- Verificar que el usuario exista en la base de datos
        //buscar un usuario en la base de datos por email
        var user = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFound);
        }

        //2.- Validar password encriptado contra password ingresado por el usuario
        
        if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash!.Value)){
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }
        

        //3.- Generar JWT y entrgarlo al cliente

        

        //return jwt;
    }

}
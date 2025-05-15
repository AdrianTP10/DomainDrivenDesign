namespace CleanArchitecture.Application.Abstractions.Authentication;

using CleanArchitecture.Domain.Users;

public interface IJwtProvider{
    Task<string> Generate(User user);
    
}
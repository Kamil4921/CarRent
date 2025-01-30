using CarRent.Domain.Users;

namespace CarRent.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
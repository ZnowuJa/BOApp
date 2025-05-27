namespace Application.Interfaces.Identity.Services;

public interface IUserRegistrationService
{
    Task CheckRoles();
    Task RegisterUserFromExternalProviderAsync(string email, string userName, string name);
}


namespace TrackMyCourseApi.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> RegisterAsync(string email, string password, string firstName, string lastName);
    
    Task<AuthenticationResult> LoginAsync(string email, string password);
}
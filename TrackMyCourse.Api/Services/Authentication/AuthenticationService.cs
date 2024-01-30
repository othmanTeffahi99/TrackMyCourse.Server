using TrackMyCourseApi.Common.Authentication;

namespace TrackMyCourseApi.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) : IAuthenticationService
{
    public AuthenticationResult Register(string email, string password, string firstName, string lastName)
    {
        //Check if the user already exists
        
        //Create the user (generate Unique id ID)
        
        // Create Jwt Token
        Guid userId = Guid.NewGuid();
        var token = jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        
        return new AuthenticationResult(userId,firstName,lastName, email,token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        throw new NotImplementedException();
    }
}
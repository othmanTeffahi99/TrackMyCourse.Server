namespace TrackMyCourseApi.Services.Authentication;

public record AuthenticationResult(Guid UserId, string FirstName, string LastName, string Email, string Token);

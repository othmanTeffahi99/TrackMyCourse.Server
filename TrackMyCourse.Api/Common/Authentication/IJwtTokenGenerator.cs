namespace TrackMyCourseApi.Common.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}
using TrackMyCourseApi.models;

namespace TrackMyCourseApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
}
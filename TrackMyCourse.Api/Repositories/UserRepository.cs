using Microsoft.EntityFrameworkCore;
using TrackMyCourseApi.Data;
using TrackMyCourseApi.models;
using TrackMyCourseApi.Repositories.Interfaces;

namespace TrackMyCourseApi.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return user;
    } 
    
}
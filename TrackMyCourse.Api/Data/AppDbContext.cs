using Microsoft.EntityFrameworkCore;
using trackmycourseapi.models;

namespace TrackMyCourseApi.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Course> Courses => Set<Course>();
}
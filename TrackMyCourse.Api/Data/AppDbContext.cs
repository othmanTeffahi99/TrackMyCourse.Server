using Microsoft.EntityFrameworkCore;
using trackmycourseapi.models;

namespace TrackMyCourseApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>().Property(x => x.State).HasConversion<int>();
            
    }

    public DbSet<Course> Courses => Set<Course>();
}
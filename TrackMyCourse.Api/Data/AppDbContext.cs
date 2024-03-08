using Microsoft.EntityFrameworkCore;
using TrackMyCourseApi.models;

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

    public DbSet<Course> Courses { get; set; }
    public DbSet<User> Users { get; set; }
}
using System.Runtime.InteropServices.JavaScript;
using trackmycourseapi.models;

namespace TrackMyCourseApi.Data;

public static class SeedData
{
    public static  void PrepData(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        AddCourses(context);
        // AddUsers(context);
    }

    private static void AddCourses(AppDbContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        if (context.Courses.Any()) return;

        Course[] courses = new []
        {
            new Course()
            {
                Id = 1,
                Name = "C#",
                Description = "C# Course",
                UpdatedAt = DateTimeOffset.UtcNow,
                Progress = 10,
                IsCompleted = false
            },
            new Course()
            {
                Id = 2,
                Name = "Java",
                Description = "Java Course",
                UpdatedAt = DateTimeOffset.UtcNow,
                Progress = 10,
                IsCompleted = false
            },
            new Course()
            {
                Id = 3,
                Name = "Python",
                Description = "Python Course",
                UpdatedAt = DateTimeOffset.UtcNow,
                Progress = 10,
                IsCompleted = false
            }
        };
            
        context.Courses.AddRange(courses);
        context.SaveChanges();
    }
}
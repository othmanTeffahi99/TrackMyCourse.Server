
using TrackMyCourseApi.Enums;


namespace TrackMyCourseApi.models;

public class User
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; } 
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public Role Role { get; set; } = Role.User;
    public required DateTimeOffset CreatedAt { get; set; }
    public required DateTimeOffset UpdatedAt { get; set; }
    public ICollection<Course> Courses { get; set; } = null!;
}
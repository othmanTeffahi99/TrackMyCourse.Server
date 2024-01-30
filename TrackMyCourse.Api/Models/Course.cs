namespace trackmycourseapi.models;

public class Course
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double? Progress { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
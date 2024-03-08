using System.ComponentModel.DataAnnotations;
using TrackMyCourseApi.Enums;

namespace TrackMyCourseApi.models;

public class Course
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double? Progress { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFavorite { get; set; }
    public CourseState State { get; set; }
    public required DateTimeOffset UpdatedAt { get; set; }
    
   
}
namespace TrackMyCourseApi.Dtos.CourseDtos;

public record CourseDto(int Id, string Name, string? Description, double? Progress, bool IsCompleted, DateTimeOffset UpdatedAt);
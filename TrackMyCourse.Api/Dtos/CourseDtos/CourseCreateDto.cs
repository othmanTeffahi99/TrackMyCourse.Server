namespace TrackMyCourseApi.Dtos.CourseDtos;

public record CourseCreateDto(string Name, string? Description, double? Progress, bool IsCompleted);

namespace TrackMyCourseApi.Dtos.CourseDtos;

public record CourseReadDto(int Id, string Name, string? Description, double? Progress, bool IsCompleted, bool IsFavorite, DateTimeOffset UpdatedAt);
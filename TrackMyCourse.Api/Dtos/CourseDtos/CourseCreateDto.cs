namespace TrackMyCourseApi.Dtos.CourseDtos;

public record CourseCreateDto(string Name, string? Description, int Progress, bool IsCompleted, bool IsFavorite,
    DateTimeOffset UpdatedAt);

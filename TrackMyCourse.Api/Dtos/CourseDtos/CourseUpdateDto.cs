using TrackMyCourseApi.Enums;

namespace TrackMyCourseApi.Dtos.CourseDtos;

public record CourseUpdateDto(int Id, string Name, string? Description, double? Progress, bool IsCompleted,
    bool IsFavorite, DateTimeOffset UpdatedAt, CourseState State);
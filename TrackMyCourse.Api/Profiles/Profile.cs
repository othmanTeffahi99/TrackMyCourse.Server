
using TrackMyCourseApi.Dtos.CourseDtos;
using TrackMyCourseApi.models;


namespace TrackMyCourseApi.Profiles;

public sealed class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Course, CourseReadDto>();
        CreateMap<CourseCreateDto, Course>();
        CreateMap<CourseUpdateDto, Course>();
    }
}
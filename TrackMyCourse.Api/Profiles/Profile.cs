﻿using TrackMyCourseApi.Dtos;
using TrackMyCourseApi.Dtos.CourseDtos;
using trackmycourseapi.models;

namespace TrackMyCourseApi.Profiles;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Course, CourseReadDto>();
        CreateMap<CourseCreateDto, Course>();
        CreateMap<CourseUpdateDto, Course>();
    }
}
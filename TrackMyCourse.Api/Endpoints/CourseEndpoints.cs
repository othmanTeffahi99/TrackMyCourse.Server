using AutoMapper;
using TrackMyCourseApi.Dtos;
using TrackMyCourseApi.Dtos.CourseDtos;
using trackmycourseapi.models;
using TrackMyCourseApi.Repositories.Interfaces;

namespace TrackMyCourseApi.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this WebApplication app)
    {
        var courseGroupBuilder = app.MapGroup("api/course").AllowAnonymous();
        courseGroupBuilder.MapGet("/", async (IRepository<Course> repository, IMapper mapper ) =>
        {
            var courses = await repository.GetAllAsync();
            var coursesDto = mapper.Map<IEnumerable<CourseDto>>(courses);
            return Results.Ok(coursesDto);
        });

        courseGroupBuilder.MapGet("/{id}", async (IRepository<Course> repository,IMapper mapper, int id) =>
        {
            var course = await repository.GetByIdAsync(id);
            var courseDto = mapper.Map<CourseDto>(course);
            return course is null ? Results.NotFound() : Results.Ok(courseDto);
        });

        courseGroupBuilder.MapPost("/", async (IRepository<Course> repository,IMapper mapper, CourseCreateDto  courseCreateDto) =>
        {
            var course = mapper.Map<Course>(courseCreateDto);
            var result = await repository.CreateAsync(course);
            return Results.Created($"/courses/{result.Id}", result);
        });

        courseGroupBuilder.MapPut("/{id}", async (IRepository<Course> repository,IMapper mapper, int id, CourseDto courseDto) =>
        {
            var course = mapper.Map<Course>(courseDto);
            //Todo: Check if the course exists
             await repository.UpdateAsync(course);
             
             //TODO : Check if the course was updated
             Results.Ok(course);
        });

        courseGroupBuilder.MapDelete("/{id}", async (IRepository<Course> repository, int id) =>
        {
            //Todo: Check if the course exists
            // await repository.DeleteAsync(course);
           // Results.Ok(course);
        });
    }
}
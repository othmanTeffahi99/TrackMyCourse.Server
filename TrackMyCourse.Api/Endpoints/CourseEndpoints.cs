using System.Net;
using System.Text.RegularExpressions;
using AutoMapper;
using TrackMyCourseApi.Dtos;
using TrackMyCourseApi.Dtos.CourseDtos;
using trackmycourseapi.models;
using TrackMyCourseApi.Repositories.Interfaces;
using FluentValidation;

namespace TrackMyCourseApi.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this WebApplication app)
    {
        var courseGroupBuilder = app.MapGroup("api/courses").AllowAnonymous();
        
        // Get All Courses Endpoint 
        courseGroupBuilder.MapGet("/", async (IRepository<Course> repository, IMapper mapper) =>
        {
            var courses = await repository.GetAllAsync();
            var coursesDto = mapper.Map<IEnumerable<CourseReadDto>>(courses);
            return Results.Ok(coursesDto);
        });
 
        // Get Course by Id Endpoint
        courseGroupBuilder.MapGet("/{id}", async (IRepository<Course> repository, IMapper mapper, int id) =>
        {
            var course = await repository.GetByIdAsync(id);
            var courseDto = mapper.Map<CourseReadDto>(course);
            return course is null ? Results.NotFound() : Results.Ok(courseDto);
        });

        // Create Course Endpoint
        courseGroupBuilder.MapPost("/",
            async (IRepository<Course> repository, IMapper mapper, IValidator<Course> validator, CourseCreateDto courseCreateDto) =>
            {
                var course = mapper.Map<Course>(courseCreateDto);
                var  validationResult = await validator.ValidateAsync(course);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }
                var result = await repository.CreateAsync(course);
                await repository.SaveChangesAsync();
                return Results.Created($"/courses/{result.Id}", result);
            });

        // Update Course Endpoint
        courseGroupBuilder.MapPut("/{id}",
            async (IRepository<Course> repository, IMapper mapper, int id, CourseUpdateDto courseUpdateDto) =>
            {
                var existingCourse = await repository.GetByIdAsync(id);
                if (existingCourse is not null)
                {
                    var course = mapper.Map<Course>(courseUpdateDto);
                    await repository.UpdateAsync(course);
                    await repository.SaveChangesAsync();
                    var courseReadDto = mapper.Map<CourseReadDto>(course);
                    return Results.Ok(courseReadDto);
                }

                return Results.NotFound("The Course that you are trying to update not found.");
            });

        // Delete Course Endpoint
        courseGroupBuilder.MapDelete("/{id}", async (IRepository<Course> repository, IMapper mapper, int id) =>
        {
            var course = await repository.GetByIdAsync(id);
            if (course is not null)
            {
                await repository.DeleteAsync(course);
                await repository.SaveChangesAsync();
                var courseReadDto = mapper.Map<CourseReadDto>(course);
                return Results.Ok(courseReadDto);
            }

            return Results.NotFound("The Course that you are trying to delete not found.");
        });
    }
}
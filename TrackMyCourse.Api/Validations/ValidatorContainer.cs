using FluentValidation;

using TrackMyCourseApi.models;

namespace TrackMyCourseApi.Validations;

public static class ValidatorContainer
{
    public static void RegisterAppValidatorContainer(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Course>, CourseValidator>();
    }
}
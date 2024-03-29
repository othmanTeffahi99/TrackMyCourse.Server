﻿using FluentValidation;
using TrackMyCourseApi.models;

namespace TrackMyCourseApi.Validations;

public class CourseValidator : AbstractValidator<Course>
{

    public CourseValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Progress).InclusiveBetween(0, 100);
        RuleFor(x => x.Description).Length(0, 200);
        RuleFor(x => x.UpdatedAt).NotNull();
        
        
    }
}
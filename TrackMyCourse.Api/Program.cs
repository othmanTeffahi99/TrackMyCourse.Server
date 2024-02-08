using System.Reflection;
using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TrackMyCourseApi.Common.Authentication;
using TrackMyCourseApi.Common.DateTimeProvider;
using TrackMyCourseApi.Data;
using TrackMyCourseApi.Endpoints;
using TrackMyCourseApi.Repositories.Interfaces;
using TrackMyCourseApi.Repositories.RepositoryBase;
using TrackMyCourseApi.Services.DateTimeProvider;
using TrackMyCourseApi.Validations;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("TrackMyCourseDb").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    ;
    opt.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

//Add option Pattern
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SETTINGS));

//Add Validation
builder.Services.RegisterAppValidatorContainer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseSerilogRequestLogging();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

//Map the endpoints
app.MapCourseEndpoints();

app.MapGet("error", (ILogger logger, HttpContext httpcontext) =>
{
    Exception? exception = httpcontext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    logger.Error(exception?.Message ?? "An error occurred.");
    Results.Problem(exception?.Message, statusCode: 500);
});


//Seed the data
SeedData.PrepData(app);

app.Run();
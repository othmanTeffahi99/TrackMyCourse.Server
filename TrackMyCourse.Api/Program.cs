using System.Reflection;
using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TrackMyCourseApi.Common.Authentication;
using TrackMyCourseApi.Common.DateTimeProvider;
using TrackMyCourseApi.Data;
using TrackMyCourseApi.Endpoints;
using TrackMyCourseApi.Repositories.Interfaces;
using TrackMyCourseApi.Repositories.RepositoryBase;
using TrackMyCourseApi.Services.DateTimeProvider;
using TrackMyCourseApi.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TrackMyCourseDb"));
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Map the endpoints
app.MapCourseEndpoints();

//Seed the data
SeedData.PrepData(app);

app.Run();



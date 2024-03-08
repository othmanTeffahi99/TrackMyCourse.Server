using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using TrackMyCourseApi.Common.Authentication;
using TrackMyCourseApi.Common.DateTimeProvider;
using TrackMyCourseApi.Data;
using TrackMyCourseApi.Endpoints;
using TrackMyCourseApi.Repositories;
using TrackMyCourseApi.Repositories.Interfaces;
using TrackMyCourseApi.Repositories.RepositoryBase;
using TrackMyCourseApi.Services.Authentication;
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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

//Add option Pattern
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SETTINGS));

//Add Validation
builder.Services.RegisterAppValidatorContainer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackMyCourseWebApi", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    
    
});

builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAuthentication().AddJwtBearer(x =>
{
    var jwtSettings = new JwtSettings();
    builder.Configuration.Bind(JwtSettings.SETTINGS, jwtSettings);
    x.TokenValidationParameters = new()
    {
       ValidIssuer = jwtSettings.Issuer,
       ValidAudience = jwtSettings.Audience,
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
       ValidateIssuer = true,
       ValidateAudience = true,
       ValidateLifetime = true,
       ValidateIssuerSigningKey = true
       
    };
});
builder.Services.AddAuthorization();

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
app.MapAuthenticationEndpoints();

app.MapGet("error", (ILogger logger, HttpContext httpcontext) =>
{
    Exception? exception = httpcontext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    logger.Error(exception?.Message ?? "An error occurred.");
    return Results.Problem(exception?.Message, statusCode: 500);
}).AllowAnonymous();




//Seed the data
SeedData.PrepData(app);

app.Run();

using TrackMyCourseApi.Dtos.AuthenticationDtos;
using TrackMyCourseApi.Services.Authentication;

namespace TrackMyCourseApi.Endpoints;

public static class AuthenticationEndPoints
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        var authGroupBuilder = app.MapGroup("/api").AllowAnonymous();
        authGroupBuilder.MapPost("/login",
            async (LoginRequestDto? loginRequestDto, IAuthenticationService authenticationService,  Serilog.ILogger logger ) =>
            {
                if (loginRequestDto is null)
                {
                    logger.Warning($"the loginRequestDto is null");
                    return Results.BadRequest("the request payload is empty");
                }

                var authenticationResult =
                    await authenticationService.LoginAsync(loginRequestDto.Email, loginRequestDto.Password);

                if (authenticationResult is not null) return Results.Ok(authenticationResult?.Token);
                logger.Warning($"something incorrect in the request payload");
                return Results.BadRequest("something incorrect in the request payload");

            });

        authGroupBuilder.MapPost("/register",
            async (RegisterRequestDto? registerRequestDto, IAuthenticationService authenticationService,
                Serilog.ILogger logger) =>
            {
                if (registerRequestDto is null)
                {
                    logger.Warning($"the registerRequestDto is null");
                    return Results.BadRequest("the request payload is empty");
                }
                
                var authenticationResult = await authenticationService.RegisterAsync(registerRequestDto.Email,
                    registerRequestDto.Password, registerRequestDto.FirstName, registerRequestDto.LastName);

                if (authenticationResult is null)
                {
                    logger.Warning($"something incorrect in the request payload");
                    return  Results.BadRequest("something incorrect in the request payload");   
                }
                
                return Results.Created();
                
            });
    }
}
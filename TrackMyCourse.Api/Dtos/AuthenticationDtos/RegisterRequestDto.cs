namespace TrackMyCourseApi.Dtos.AuthenticationDtos;

public sealed record RegisterRequestDto(string FirstName, string LastName,string Email,string Password);
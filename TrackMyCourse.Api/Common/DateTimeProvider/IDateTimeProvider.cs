namespace TrackMyCourseApi.Common.DateTimeProvider;

public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
}
using TrackMyCourseApi.Common.DateTimeProvider;

namespace TrackMyCourseApi.Services.DateTimeProvider;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now  => DateTimeOffset.Now;
}
namespace RaceIntel.Api.Admin.Import;

public record NascarWeekendFeedImportRequest(int Year, int SeriesId, int RaceId, bool Force = false);
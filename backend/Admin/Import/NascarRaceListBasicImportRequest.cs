namespace RaceIntel.Api.Admin.Import;

public record NascarRaceListBasicImportRequest(int Year, bool Force = false);
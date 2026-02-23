namespace RaceIntel.Api.Data.Entities;

public class NascarWeekendFeed
{
    public long Id { get; set; }                  //primary key
    public int Year { get; set; }
    public int SeriesId { get; set; }
    public int RaceId { get; set; }

    public DateTime PulledAtUtc { get; set; }
    public string RawJson { get; set; } = "";    //store raw json for flexibility, can be parsed into structured data as needed
}
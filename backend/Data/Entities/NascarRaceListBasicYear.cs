namespace RaceIntel.Api.Data.Entities;

public class NascarRaceListBasicYear
{
    public int Year { get; set; }              //primary key
    public DateTime PulledAtUtc { get; set; }
    public string RawJson { get; set; } = "";  //store raw json for flexibility, can be parsed into structured data as needed
}
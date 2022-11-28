namespace ReportService.Worker;

public class ReportVm
{
    public int ReportId { get; set; }
    public List<LocationData> Data { get; set; } = new ();
}

public class LocationData
{
    public string Location { get; set; }
    public int UserCount { get; set; }
    public int TelephoneCount { get; set; }
}
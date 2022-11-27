
using ServiceCommon.BaseEntity;

namespace ReportService.Api.Entities;

public class Report : Entity
{
    public string Status { get; set; }
    public string ReportData { get; set; }
}
namespace Application.Features.Events.Queries.GetEventsExport;

public class ExportEventsFileResponseDto
{
    public string EventExportFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty; // CSV
    public byte[]? Data { get; set; }
}
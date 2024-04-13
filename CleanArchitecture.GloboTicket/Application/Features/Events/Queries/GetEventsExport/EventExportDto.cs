namespace Application.Features.Events.Queries.GetEventsExport;

public abstract class EventExportDto
{
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public string? Artist { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
}
using System.Globalization;
using Application.Contracts.Infrastructure;
using Application.Features.Events.Queries.GetEventsExport;
using CsvHelper;

namespace Infrastructure.FileExport;

public class CsvExporter : ICsvExporter
{
    public byte[]? ExportEventsToCsv(List<EventExportDto> allEvents)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(allEvents);
        }

        return memoryStream.ToArray();
    }
}
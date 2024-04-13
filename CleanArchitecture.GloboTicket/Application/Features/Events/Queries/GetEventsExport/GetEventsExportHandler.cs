using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Events.Queries.GetEventsExport;

public class GetEventsExportHandler: IRequestHandler<GetEventsExportQuery, ExportEventsFileResponseDto?>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Event> _eventRepository;
    private readonly ICsvExporter _csvExporter;

    public GetEventsExportHandler(IMapper mapper, IEventRepository eventRepository, ICsvExporter csvExporter)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
        _csvExporter = csvExporter;
    }

    public async Task<ExportEventsFileResponseDto?> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allEvents = _mapper.Map<List<EventExportDto>>((await _eventRepository.ListAllAsync()).OrderBy(x => x.Date));

            var fileData = _csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDto = new ExportEventsFileResponseDto()
            {
                ContentType = "text/csv",
                Data = fileData,
                EventExportFileName = $"{Guid.NewGuid()}.csv"
            };

            return eventExportFileDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}
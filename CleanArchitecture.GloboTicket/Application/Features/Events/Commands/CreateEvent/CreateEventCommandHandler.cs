using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Models.Mail;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CreateEventCommandHandler> _logger;

    public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService,
        ILogger<CreateEventCommandHandler> logger)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = _mapper.Map<Event>(request);

        var validator = new CreateEventCommandValidator(_eventRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"Invalid request {typeof(CreateEventCommand)} ", validationResult.Errors);
        }

        var eventId = (await _eventRepository.AddAsync(@event)).EventId;

        var email = new Email() { To = "asdf@email", Body = $"A new event was created: {request}" };
        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            _logger.LogError("Failed to send email :(");
        }

        return eventId;
    }
}
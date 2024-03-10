using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Models.Mail;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = _mapper.Map<Event>(request);

        var validator = new CreateEventCommandValidator(_eventRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }

        var eventId = (await _eventRepository.AddAsync(@event)).EventId;
        
        var email = new Email() { To = "asdf@email", Body = $"A new event was created: {request}" };
        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return eventId;
    }
}
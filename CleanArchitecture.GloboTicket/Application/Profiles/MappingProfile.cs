using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Queries.GetCategoriesList;
using Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using Application.Features.Events.Commands.CreateEvent;
using Application.Features.Events.Queries.GetEventDetail;
using Application.Features.Events.Queries.GetEventsList;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<Event, EventDetailDto>().ReverseMap();
        CreateMap<Category, EventDetailCategoryDto>().ReverseMap();

        CreateMap<Category, CategoryWithEventDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();

        CreateMap<Event, CreateEventCommand>().ReverseMap();
        // CreateMap<Event, UpdateEventCommand>().ReverseMap();
        // CreateMap<Event, CategoryEventDto>().ReverseMap();
        // ----
    }
}
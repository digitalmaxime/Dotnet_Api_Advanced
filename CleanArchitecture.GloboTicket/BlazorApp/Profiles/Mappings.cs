using AutoMapper;
using GloboTicket.TicketManagement.App.Services;
using GloboTicket.TicketManagement.App.ViewModels;
using MyNamespace;

namespace GloboTicket.TicketManagement.App.Profiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            //Vms are coming in from the API, ViewModel are the local entities in Blazor
            CreateMap<EventDto, EventListViewModel>().ReverseMap();
            CreateMap<EventDetailDto, EventDetailViewModel>().ReverseMap();
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<CategoryWithEventDto, CategoryEventsViewModel>().ReverseMap();

            CreateMap<EventDetailViewModel, CreateEventCommand>().ReverseMap();
            CreateMap<EventDetailViewModel, UpdateEventCommand>().ReverseMap();

            CreateMap<CategoryEventDto, EventNestedViewModel>().ReverseMap();

            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryCommand, CategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryDto, CategoryDto>().ReverseMap();

            CreateMap<PagedOrdersForMonthVm, PagedOrderForMonthViewModel>().ReverseMap();
            CreateMap<OrdersForMonthDto, OrdersForMonthListViewModel>().ReverseMap();
        }
    }
}

using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoriesListWithEvents;

public class
    GetCategoriesListWithEventsQueryHandler : IRequestHandler<GetCategoriesListWithEventsQuery, List<CategoryWithEventDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesListWithEventsQueryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryWithEventDto>> Handle(
        GetCategoriesListWithEventsQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetCategoriesWithEvents(request.IncludeHistory);
        var categoriesDto = _mapper.Map<List<CategoryWithEventDto>>(categories);

        return categoriesDto;
    }
}
using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoriesList;

public class GetCategoriesListQueryHandler: IRequestHandler<GetCategoriesListQuery, List<CategoryDto>>
{
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesListQueryHandler(IAsyncRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<List<CategoryDto>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var categories = (await _categoryRepository.ListAllAsync()).OrderBy(x => x.Name);

        return _mapper.Map<List<CategoryDto>>(categories);
    }
}
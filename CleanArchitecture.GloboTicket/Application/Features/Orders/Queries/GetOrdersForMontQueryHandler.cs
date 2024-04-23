using MediatR;

namespace Application.Features.Orders.Queries;

public class GetOrdersForMontQueryHandler: IRequestHandler<GetOrdersForMonthQuery, PagedOrdersForMonthVm>
{
    public Task<PagedOrdersForMonthVm> Handle(GetOrdersForMonthQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
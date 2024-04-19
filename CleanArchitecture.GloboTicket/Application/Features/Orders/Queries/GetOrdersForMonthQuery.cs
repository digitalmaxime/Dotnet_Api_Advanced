using MediatR;

namespace Application.Features.Orders.Queries;

public record GetOrdersForMonthQuery(DateTime Date, int Page, int Size) : IRequest<PagedOrdersForMonthVm>;

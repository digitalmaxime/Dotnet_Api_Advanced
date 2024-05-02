using Domain.Entities;

namespace Application.Features.Orders.Queries;

public class OrdersForMonthDto
{
    public Guid Id { get; set; }
    public int OrderTotal { get; set; }
    public DateTime OrderPlaced { get; set; }
}
using Domain.Common;

namespace Domain.Entities;

public class Order: AuditableEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int OderTotal { get; set; }
    public DateTime OrderPlaced { get; set; }
    public bool OrderPaid { get; set; }
}
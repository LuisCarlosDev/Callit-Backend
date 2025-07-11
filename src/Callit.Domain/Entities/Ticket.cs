using Callit.Domain.Enums;

namespace Callit.Domain.Entities;

public class Ticket
{
  public Guid Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public TicketStatus Status { get; set; } = TicketStatus.Waiting;

  public string System { get; set; } = string.Empty;

  public Priority Priority { get; set; }

  public DateTime Date { get; set; }

  public Guid UserId { get; set; }

  public User User { get; set; } = default!;
}

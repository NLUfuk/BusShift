using System.ComponentModel.DataAnnotations;

namespace BusShift.Models;
public class Ticket : BaseEntity
{
    public required string TicketNumber { get; set; }
    public required DateTime IssueDate { get; set; }
    public required DateTime ExpiryDate { get; set; }

    [Precision(8, 2)]
    public decimal Price { get; set; }
    public string? Notes { get; set; }

    // Navigation properties    
    public required Passenger Passenger { get; set; } = null!;
    public Guid? PassengerId { get; set; }

    public required ShiftAssignment ShiftAssignment { get; set; } = null!;
    public Guid? ShiftAssignmentId { get; set; }

    public required Seat Seat { get; set; } = null!;
    public Guid? SeatId { get; set; }
}
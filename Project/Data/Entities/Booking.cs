using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Booking
{
    [Key]
    public int BookingId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    [Required]
    [StringLength(50)]
    public string? Status { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    [ForeignKey("RoomId")]
    public Room Room { get; set; }
}

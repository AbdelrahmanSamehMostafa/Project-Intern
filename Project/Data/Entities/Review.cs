using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review
{
    [Key]
    public int ReviewId { get; set; }

    [Required]
    [StringLength(1000)]
    public string Comment { get; set; }

    [Required]
    public int Rating { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    public int CustomerId { get; set; }
    
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    public int HotelId { get; set; }

    [ForeignKey("HotelId")]
    public Hotel Hotel { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Room
{
    [Key]
    public int RoomId { get; set; }

    [Required]
    public int HotelId { get; set; }

    [ForeignKey("HotelId")]
    public Hotel Hotel { get; set; }

    [Required]
    public RoomType RoomType { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [Range(0, 10000)]
    public double Price { get; set; }

    [Required]
    public bool isAvailable { get; set; }
}

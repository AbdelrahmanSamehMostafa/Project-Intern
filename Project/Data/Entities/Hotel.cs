using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Hotel
{
    [Key]
    public int HotelId { get; set; }

    public int  AdminId { get; set; }
    
    [Required]
    [ForeignKey("AdminId")]
    public Admin HotelOwner { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
    public string Name { get; set; }

    public Address Address { get; set; }

    [Range(1.0, 5.0, ErrorMessage = "Rating must be between 1.0 and 5.0")]
    public double Rating { get; set; }

    //[Required]
    [MinLength(1, ErrorMessage = "At least one room is required.")]
    public List<Room> Rooms { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Total number of rooms must be at least 1")]
    public int TotalNumberOfRooms { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Number of available rooms cannot be negative")]
    public int NumberOfAvailableRooms { get; set; }

    public List<string>? Entertainments { get; set; }

    [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
    public string? Description { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Contact information can't be longer than 100 characters.")]
    [RegularExpression(@"(^\+[0-9]{1,3})?(\([0-9]{1,4}\))?[-. \\\/]?(\(?[0-9]+\)?[-. \\\/\\/]?)+$", ErrorMessage = "Invalid contact info format.")]
    public string ContactInfo { get; set; }

    public List<string>? ImageUrls { get; set; }
    public List<Review>? Reviews { get; set; }

    public bool IsActive { get; set; }
}
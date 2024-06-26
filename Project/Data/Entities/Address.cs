using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Address
{
    [Key]
    public int AddressId { get; set; }

    [Required]
    [StringLength(50)]
    public string? City { get; set; }

    [Required]
    [StringLength(50)]
    public string? Country { get; set; }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Admin
{
    [Key]
    public int AdminId { get; set; }

    [ForeignKey("SuperAdminId")]
    public SuperAdmin? SuperAdmin { get; set; }

    public int? SuperAdminId { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string LastName { get; set; }
  

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    public bool IsActive { get; set; } = false;

    public ICollection<Hotel>? Hotels { get; set; }
}


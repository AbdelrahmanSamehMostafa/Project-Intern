using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

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

    public bool IsEmailVerified { get; set; }

}



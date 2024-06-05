using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Azure.Core;

public class SuperAdmin
{
    [Key]
    public int SuperAdminId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; private set; } = "admin";

    [Required]
    [StringLength(100)]
    public string Password { get; private set; } = "admin";

    //public List<PendingReq>? PendingReqs { get; set; } = new List<PendingReq>();

    public SuperAdmin(string name, string password)
    {
        SuperAdminId = 1;
        Name = name;
        Password = password;
    }
    private SuperAdmin() {}

    public void SetName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }
    }
    public void SetPassword(string password)
    {
        if (!string.IsNullOrWhiteSpace(password))
        {
            Password = password;
        }
    }
}

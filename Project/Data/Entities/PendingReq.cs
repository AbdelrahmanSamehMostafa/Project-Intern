using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PendingReq
{
    [Key]
    public int PendingReqId { get; set; }
    public int SuperAdminID { get; set; }
    public int AdminID { get; set; }           
}

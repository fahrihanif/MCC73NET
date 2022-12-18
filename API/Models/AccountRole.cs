using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class AccountRole
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string AccountNIK { get; set; }
    [Required]
    public int RoleId { get; set; }

    // Relation
    [ForeignKey("AccountNIK")]
    public Account? Account { get; set; }
    [ForeignKey("RoleId")]
    public Role? Role { get; set; }
}

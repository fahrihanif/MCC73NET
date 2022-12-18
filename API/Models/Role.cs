using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    // Relation
    public ICollection<AccountRole>? AccountRoles { get; set; }
}

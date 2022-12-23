using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_m_roles")]
public class Role
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("name"), MaxLength(50)]
    public string Name { get; set; }

    [JsonIgnore]
    // Relation
    public ICollection<AccountRole>? AccountRoles { get; set; }
}

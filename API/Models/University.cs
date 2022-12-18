using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class University
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    // Relation
    public ICollection<Education>? Educations { get; set; }
}

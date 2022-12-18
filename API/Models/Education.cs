using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Education
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Degree { get; set; }
    [Required]
    public string GPA { get; set; }
    [Required]
    public int UniversityId { get; set; }

    // Relation
    [ForeignKey("UniversityId")]
    public University? University { get; set; }
    public Profiling? Profiling { get; set; }
}

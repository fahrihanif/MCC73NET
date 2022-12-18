using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Profiling
{
    [Key]
    public string NIK { get; set; }
    [Required]
    public int EducationId { get; set; }

    // Relation
    [ForeignKey("EducationId")]
    public Education? Education { get; set; }
    public Account? Account { get; set; }
}

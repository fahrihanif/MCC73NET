using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_m_educations")]
public class Education
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("degree"), MaxLength(50)]
    public string Degree { get; set; }
    [Required, Column("gpa"), MaxLength(5)]
    public string GPA { get; set; }
    [Required, Column("university_id")]
    public int UniversityId { get; set; }

    [JsonIgnore]
    // Relation
    [ForeignKey("UniversityId")]
    public University? University { get; set; }
    [JsonIgnore]
    public Profiling? Profiling { get; set; }
}

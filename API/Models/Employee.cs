using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Employee
{
    [Key]
    public string NIK { get; set; }
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public int Salary { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public Gender Gender { get; set; }

    // Relation
    public Account? Account { get; set; }
}

public enum Gender
{
    Male,
    Female
}

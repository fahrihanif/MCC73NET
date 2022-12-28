using API.Models;

namespace API.ViewModels;

public class MEmployeeVM
{
    public string NIK { get; set; }

    public string FullName { get; set; }

    public string Phone { get; set; }

    public string Gender { get; set; }

    public string Email { get; set; }
    public List<Role> Role { get; set; }

    public DateTime BirthDate { get; set; }

    public int Salary { get; set; }

    public string GPA { get; set; }

    public string Degree { get; set; }

    public string UniversityName { get; set; }
}

public class Role
{
    public string Name { get; set; }
}
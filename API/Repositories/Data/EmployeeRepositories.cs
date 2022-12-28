using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;

namespace API.Repositories.Data;

public class EmployeeRepositories : GeneralRepository<MyContext, Employee, string>
{
    public MyContext _context;
    public EmployeeRepositories(MyContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<MEmployeeVM> MasterEmployee()
    {
        var results = (from e in _context.Employees
                       join a in _context.Accounts on e.NIK equals a.NIK
                       join ar in _context.AccountRoles on a.NIK equals ar.AccountNIK
                       join r in _context.Roles on ar.RoleId equals r.Id
                       join p in _context.Profilings on e.NIK equals p.NIK
                       join edu in _context.Educations on p.EducationId equals edu.Id
                       join u in _context.Universities on edu.UniversityId equals u.Id
                       select new MEmployeeVM
                       {
                           NIK = e.NIK,
                           FullName = e.FirstName + " " + e.LastName,
                           Phone = e.Phone,
                           Gender = e.Gender.ToString(),
                           Email = e.Email,
                           BirthDate = e.BirthDate,
                           Salary = e.Salary,
                           Role = _context.AccountRoles.Where(e => e.AccountNIK == p.NIK).Join(_context.Roles, ar => ar.RoleId, r => r.Id, (ar, r) => new ViewModels.Role { Name = r.Name }).ToList(),
                           GPA = edu.GPA,
                           Degree = edu.Degree,
                           UniversityName = u.Name
                       }).ToList();

        return results;
    }
}
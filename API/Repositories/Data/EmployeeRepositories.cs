using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;

namespace API.Repositories.Data;

public class EmployeeRepositories : IRepository<Employee, string>
{
    private MyContext _context;
    private DbSet<Employee> _employees;
    public EmployeeRepositories(MyContext context)
    {
        _context = context;
        _employees = context.Set<Employee>();
    }

    public IEnumerable<MEmployeeVM> MasterEmployee()
    {
        var result = _employees
                .Join(_context.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a })
                .Join(_context.Profilings, ea => ea.a.NIK, p => p.NIK, (ea, p) => new { ea, p })
                .Join(_context.Educations, eap => eap.p.EducationId, ed => ed.Id, (eap, ed) => new { eap, ed })
                .Join(_context.Universities, eaped => eaped.ed.UniversityId, u => u.Id, (eaped, u) =>
                new MEmployeeVM
                {
                    NIK = eaped.eap.ea.e.NIK,
                    FullName = eaped.eap.ea.e.FirstName + " " + eaped.eap.ea.e.LastName,
                    Phone = eaped.eap.ea.e.Phone,
                    Gender = eaped.eap.ea.e.Gender.ToString(),
                    Email = eaped.eap.ea.e.Email,
                    BirthDate = eaped.eap.ea.e.BirthDate,
                    Salary = eaped.eap.ea.e.Salary,
                    EducationId = eaped.eap.p.EducationId,
                    GPA = eaped.ed.GPA,
                    Degree = eaped.ed.Degree,
                    UniversityName = u.Name
                }).ToList();

        return result;
    }
    public int Delete(string id)
    {
        var data = _employees.Find(id);
        if (data == null)
        {
            return 0;
        }

        _employees.Remove(data);
        var result = _context.SaveChanges();
        return result;
    }

    public IEnumerable<Employee> Get()
    {

        return _employees.ToList();
    }

    public Employee Get(string id)
    {
        return _employees.Find(id);
    }

    public int Insert(Employee entity)
    {
        _employees.Add(entity);
        var result = _context.SaveChanges();
        return result;
    }

    public int Update(Employee entity)
    {
        _employees.Entry(entity).State = EntityState.Modified;
        var result = _context.SaveChanges();
        return result;
    }
}
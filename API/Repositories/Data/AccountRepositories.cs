using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.ViewModels;
using API.Repositories.Interface;
using API.Handlers;

namespace API.Repositories.Data;

public class AccountRepositories : GeneralRepository<MyContext, Account, string>
{
    private readonly MyContext _context;
    public AccountRepositories(MyContext context) : base(context)
    {
        _context = context;
    }

    public int Register(RegisterVM register)
    {
        register.NIK = GenerateNIK();

        if (!CheckEmailPhone(register.Email, register.Phone))
        {
            return 0; // Email atau Password sudah terdaftar
        }

        Employee employee = new Employee()
        {
            NIK = register.NIK,
            FirstName = register.FirstName,
            LastName = register.LastName,
            Phone = register.Phone,
            Gender = register.Gender,
            BirthDate = register.BirthDate,
            Salary = register.Salary,
            Email = register.Email,
        };
        _context.Employees.Add(employee);
        _context.SaveChanges();

        Account account = new Account()
        {
            NIK = register.NIK,
            Password = Hashing.HashPassword(register.Password),
        };
        _context.Accounts.Add(account);
        _context.SaveChanges();

        University university = new University()
        {
            Name = register.UniversityName
        };
        _context.Universities.Add(university);
        _context.SaveChanges();

        Education education = new Education()
        {
            Degree = register.Degree,
            GPA = register.GPA,
            UniversityId = university.Id,
        };
        _context.Educations.Add(education);
        _context.SaveChanges();

        Profiling profiling = new Profiling()
        {
            NIK = register.NIK,
            EducationId = education.Id
        };
        _context.Profilings.Add(profiling);
        _context.SaveChanges();

        _context.AccountRoles.Add(new AccountRole()
        {
            AccountNIK = register.NIK,
            RoleId = 1
        });
        _context.SaveChanges();

        return 1;
    }

    public int Login(LoginVM login)
    {
        var result = _context.Accounts.Join(_context.Employees, a => a.NIK, e => e.NIK, (a, e) =>
        new LoginVM
        {
            Email = e.Email,
            Password = a.Password
        }).SingleOrDefault(c => c.Email == login.Email);

        if (result == null)
        {
            return 0; // Email Tidak Terdaftar
        }
        if (!Hashing.ValidatePassword(login.Password, result.Password))
        {
            return 1; // Password Salah
        }
        return 2; // Email dan Password Benar
    }

    private bool CheckEmailPhone(string email, string phone)
    {
        var duplicate = _context.Employees.Where(s => s.Email == email || s.Phone == phone).ToList();

        if (duplicate.Any())
        {
            return false; // Email atau Password sudah ada
        }
        return true; // Email dan Password belum terdaftar
    }

    private string GenerateNIK()
    {
        var empCount = _context.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();

        if (empCount == null)
        {
            return "x1111";
        }
        string NIK = empCount.NIK.Substring(1, 4);
        return Convert.ToString("x" + (Convert.ToInt32(NIK) + 1));
    }

    public List<string> UserRoles(string email)
    {
        var getNIK = _context.Employees.SingleOrDefault(e => e.Email == email).NIK;
        var getRoles = _context.AccountRoles.Where(ar => ar.AccountNIK == getNIK)
                                            .Join(_context.Roles, ar => ar.RoleId, r => r.Id, (ar, r) => r.Name)
                                            .ToList();

        return getRoles;
    }
}
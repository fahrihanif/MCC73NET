using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;

namespace API.Repositories.Data;

public class EducationRepositories : IRepository<Education, int>
{
    private MyContext _context;
    private DbSet<Education> _educations;
    private readonly DbSet<University> _universities;
    public EducationRepositories(MyContext context)
    {
        _context = context;
        _educations = context.Set<Education>();
        _universities = context.Set<University>();
    }


    public IEnumerable<MEducationVM> MasterEducation()
    {
        var result = _educations.Join(_universities, e => e.UniversityId, u => u.Id, (e, u) => new MEducationVM
        {
            Id = e.Id,
            Degree = e.Degree,
            GPA = e.GPA,
            UniversityName = u.Name
        });

        return result;
    }
    public int Delete(int id)
    {
        var data = _educations.Find(id);
        if (data == null)
        {
            return 0;
        }

        _educations.Remove(data);
        var result = _context.SaveChanges();
        return result;
    }

    public IEnumerable<Education> Get()
    {

        return _educations.ToList();
    }

    public Education Get(int id)
    {
        return _educations.Find(id);
    }

    public int Insert(Education entity)
    {
        _educations.Add(entity);
        var result = _context.SaveChanges();
        return result;
    }

    public int Update(Education entity)
    {
        _educations.Entry(entity).State = EntityState.Modified;
        var result = _context.SaveChanges();
        return result;
    }
}

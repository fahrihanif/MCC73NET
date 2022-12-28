using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;

namespace API.Repositories.Data;

public class EducationRepositories : GeneralRepository<MyContext, Education, int>
{
    private MyContext _context;

    public EducationRepositories(MyContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<MEducationVM> MasterEducation()
    {
        var result = _context.Educations.Join(_context.Universities, e => e.UniversityId, u => u.Id, (e, u) => new MEducationVM
        {
            Id = e.Id,
            Degree = e.Degree,
            GPA = e.GPA,
            UniversityName = u.Name
        });

        return result;
    }
}

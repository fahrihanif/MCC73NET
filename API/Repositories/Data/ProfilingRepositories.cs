using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data;

public class ProfilingRepositories : IRepository<Profiling, string>
{
    private MyContext _context;
    private DbSet<Profiling> _profilings;
    public ProfilingRepositories(MyContext context)
    {
        _context = context;
        _profilings = context.Set<Profiling>();
    }
    public int Delete(string id)
    {
        var data = _profilings.Find(id);
        if (data == null)
        {
            return 0;
        }

        _profilings.Remove(data);
        var result = _context.SaveChanges();
        return result;
    }

    public IEnumerable<Profiling> Get()
    {
        return _profilings.ToList();
    }

    public Profiling Get(string id)
    {
        return _profilings.Find(id);
    }

    public int Insert(Profiling entity)
    {
        _profilings.Add(entity);
        var result = _context.SaveChanges();
        return result;
    }

    public int Update(Profiling entity)
    {
        _profilings.Entry(entity).State = EntityState.Modified;
        var result = _context.SaveChanges();
        return result;
    }
}
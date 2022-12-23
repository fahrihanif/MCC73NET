using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data;

public class AccountRoleRepositories : IRepository<AccountRole, int>
{
    private MyContext _context;
    private DbSet<AccountRole> _accountroles;
    public AccountRoleRepositories(MyContext context)
    {
        _context = context;
        _accountroles = context.Set<AccountRole>();
    }
    public int Delete(int id)
    {
        var data = _accountroles.Find(id);
        if (data == null)
        {
            return 0;
        }

        _accountroles.Remove(data);
        var result = _context.SaveChanges();
        return result;
    }

    public IEnumerable<AccountRole> Get()
    {
        return _accountroles.ToList();
    }

    public AccountRole Get(int id)
    {
        return _accountroles.Find(id);
    }

    public int Insert(AccountRole entity)
    {
        _accountroles.Add(entity);
        var result = _context.SaveChanges();
        return result;
    }

    public int Update(AccountRole entity)
    {
        _accountroles.Entry(entity).State = EntityState.Modified;
        var result = _context.SaveChanges();
        return result;
    }
}
using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data;

public class ProfilingRepositories : GeneralRepository<MyContext, Profiling, string>
{
    public ProfilingRepositories(MyContext context) : base(context)
    {
    }
}
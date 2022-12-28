using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data;

public class UniversityRepositories : GeneralRepository<MyContext, University, int>
{
    public UniversityRepositories(MyContext context) : base(context)
    {
    }
}
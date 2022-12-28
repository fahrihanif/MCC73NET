using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data;

public class RoleRepositories : GeneralRepository<MyContext, Role, int>
{
    public RoleRepositories(MyContext context) : base(context)
    {
    }
}
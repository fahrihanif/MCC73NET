using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data;

public class AccountRoleRepositories : GeneralRepository<MyContext, AccountRole, int>
{
    public AccountRoleRepositories(MyContext context) : base(context)
    {
    }
}
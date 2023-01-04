using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AccountRolesController : BaseController<AccountRoleRepositories, AccountRole, int>
{
    public AccountRolesController(AccountRoleRepositories repo) : base(repo)
    {
    }
}

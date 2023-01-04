using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class RolesController : BaseController<RoleRepositories, Role, int>
{
    public RolesController(RoleRepositories repo) : base(repo)
    {
    }
}

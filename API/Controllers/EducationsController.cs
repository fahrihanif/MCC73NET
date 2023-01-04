using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Manager")]
public class EducationsController : BaseController<EducationRepositories, Education, int>
{
    public EducationsController(EducationsController repo) : base(repo)
    {
    }
}

using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Manager")]
public class UniversitiesController : BaseController<UniversityRepositories, University, int>
{
    public UniversitiesController(UniversityRepositories repo) : base(repo)
    {
    }
}

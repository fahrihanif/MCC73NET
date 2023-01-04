using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeesController : BaseController<EmployeeRepositories, Employee, string>
{
    private EmployeeRepositories _repo;
    public EmployeesController(EmployeeRepositories repo) : base(repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Route("Master")]
    [AllowAnonymous]
    public ActionResult GetMaster()
    {
        try
        {
            var result = _repo.MasterEmployee();
            return result.Count() == 0
            ? Ok(new { statusCode = 204, message = "Data Not Found!" })
            : Ok(new { statusCode = 201, message = "Success", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }

    }
}

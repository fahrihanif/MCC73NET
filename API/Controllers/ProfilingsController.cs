using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Manager")]
public class ProfilingsController : ControllerBase
{
    private ProfilingRepositories _repo;

    public ProfilingsController(ProfilingRepositories repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        try
        {
            var result = _repo.Get();
            return result.Count() == 0
            ? Ok(new { statusCode = 204, message = "Data Not Found!" })
            : Ok(new { statusCode = 201, message = "Success", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }

    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult GetById(string id)
    {
        try
        {
            var result = _repo.Get(id);
            return result == null
            ? Ok(new { statusCode = 204, message = $"Data With Id {id} Not Found!" })
            : Ok(new { statusCode = 201, message = $"NIK {id}", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }
    }

    [HttpPost]
    public ActionResult Insert(Profiling profiling)
    {
        try
        {
            var result = _repo.Insert(profiling);
            return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
            Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "" });
        }
    }

    [HttpPut]
    public ActionResult Update(Profiling profiling)
    {
        try
        {
            var result = _repo.Update(profiling);
            return result == 0 ?
            Ok(new { statusCode = 204, message = $"NIK {profiling.NIK} not found!" }) :
            Ok(new { statusCode = 201, message = "Update Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong!" });
        }
    }

    [HttpDelete]
    public ActionResult Delete(string id)
    {
        try
        {
            var result = _repo.Delete(id);
            return result == 0 ? Ok(new { statusCode = 204, message = $"NIK {id} Data Not Found" }) :
            Ok(new { statusCode = 201, message = "Data Delete Succesfully!" });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong {e.Message}" });
        }
    }
}

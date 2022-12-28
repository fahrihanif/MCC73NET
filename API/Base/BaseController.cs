using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController<Repository, Entity, Key> : ControllerBase
    where Entity : class
    where Repository : IRepository<Entity, Key>
{
    private Repository _repo;

    public BaseController(Repository repo)
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
    public ActionResult GetById(Key key)
    {
        try
        {
            var result = _repo.Get(key);
            return result == null
            ? Ok(new { statusCode = 204, message = $"Data With Id {key} Not Found!" })
            : Ok(new { statusCode = 201, message = $"Data Found!", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }
    }

    [HttpPost]
    public ActionResult Insert(Entity entity)
    {
        try
        {
            var result = _repo.Insert(entity);
            return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
            Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "" });
        }
    }

    [HttpPut]
    public ActionResult Update(Entity entity)
    {
        try
        {
            var result = _repo.Update(entity);
            return result == 0 ?
            Ok(new { statusCode = 204, message = $"Id not found!" })
          : Ok(new { statusCode = 201, message = "Update Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong!" });
        }
    }

    [HttpDelete]
    public ActionResult Delete(Key key)
    {
        try
        {
            var result = _repo.Delete(key);
            return result == 0 ? Ok(new { statusCode = 204, message = $"Id {key} Data Not Found" }) :
            Ok(new { statusCode = 201, message = "Data Delete Succesfully!" });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong {e.Message}" });
        }
    }
}

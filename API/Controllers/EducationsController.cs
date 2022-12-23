using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories.Data;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EducationsController : ControllerBase
{
  private EducationRepositories _repo;

  public EducationsController(EducationRepositories repo)
  {
    _repo = repo;
  }

  [HttpGet]
  [Route("Master")]
  public ActionResult GetMaster()
  {
    try
    {
      var result = _repo.MasterEducation();
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
  public ActionResult GetById(int id)
  {
    try
    {
      var result = _repo.Get(id);
      return result == null
      ? Ok(new { statusCode = 204, message = $"Data With Id {id} Not Found!" })
      : Ok(new { statusCode = 201, message = "Succes", data = result });
    }
    catch (Exception e)
    {
      return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
    }
  }

  [HttpPost]
  public ActionResult Insert(Education education)
  {
    try
    {
      var result = _repo.Insert(education);
      return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
      Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
    }
    catch
    {
      return BadRequest(new { statusCode = 500, message = "" });
    }
  }

  [HttpPut]
  public ActionResult Update(Education education)
  {
    try
    {
      var result = _repo.Update(education);
      return result == 0 ?
      Ok(new { statusCode = 204, message = $"Id {education.Id} not found!" })
    : Ok(new { statusCode = 201, message = "Update Succesfully!" });
    }
    catch
    {
      return BadRequest(new { statusCode = 500, message = "Something Wrong!" });
    }
  }

  [HttpDelete]

  public ActionResult Delete(int id)
  {
    try
    {
      var result = _repo.Delete(id);
      return result == 0 ? Ok(new { statusCode = 204, message = $"Id {id} Data Not Found" }) :
      Ok(new { statusCode = 201, message = "Data Delete Succesfully!" });
    }
    catch (Exception e)
    {
      return BadRequest(new { statusCode = 500, message = $"Something Wrong {e.Message}" });
    }
  }
}

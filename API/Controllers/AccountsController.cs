using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.ViewModels;
using API.Repositories.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AccountsController : ControllerBase
{
    private AccountRepositories _repo;
    private IConfiguration _con;

    public AccountsController(AccountRepositories repo, IConfiguration con)
    {
        _repo = repo;
        _con = con;
    }

    [HttpPost]
    [Route("Register")]
    public ActionResult Register(RegisterVM registerVM)
    {
        try
        {
            var result = _repo.Register(registerVM);
            return result == 0 
                ? Ok(new { statusCode = 204, message = "Email or Phone is Already Registered!" }) 
                : Ok(new { statusCode = 201, message = "Register Succesfully!" });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }
    }

    [HttpPost]
    [Route("Login")]
    public ActionResult Login(LoginVM loginVM)
    {
        try
        {
            var result = _repo.Login(loginVM);
            switch (result)
            {
                case 0:
                    return Ok(new { statusCode = 200, message = "Account Not Found!" });
                case 1:
                    return Ok(new { statusCode = 200, message = "Wrong Password!" });
                default:
                    // bikin method untuk mendapatkan role-nya user yang login
                    var roles = _repo.UserRoles(loginVM.Email);

                    var claims = new List<Claim>()
                    {
                        new Claim("email", loginVM.Email),
                        //new Claim(ClaimTypes.Email, loginVM.Email)
                    };

                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_con["JWT:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _con["JWT:Issuer"],
                        _con["JWT:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signIn
                        );

                    var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", generateToken.ToString()));

                    return Ok(new { statusCode = 200, message = "Login Success!", data = generateToken });
            }
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
    public ActionResult GetById(string id)
    {
        try
        {
            var result = _repo.Get(id);
            return result == null
            ? Ok(new { statusCode = 204, message = $"Data With Id {id} Not Found!" })
            : Ok(new { statusCode = 201, message = $"Account with NIK {id}", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }
    }

    [HttpPost]
    public ActionResult Insert(Account account)
    {
        try
        {
            var result = _repo.Insert(account);
            return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
            Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "" });
        }
    }

    [HttpPut]
    public ActionResult Update(Account account)
    {
        try
        {
            var result = _repo.Update(account);
            return result == 0 ?
            Ok(new { statusCode = 204, message = $"NIK {account.NIK} not found!" }) :
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
            return result == 0 ? Ok(new { statusCode = 204, message = $"Id {id} Data Not Found" }) :
            Ok(new { statusCode = 201, message = "Data Delete Succesfully!" });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong {e.Message}" });
        }
    }
}

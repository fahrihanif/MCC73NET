using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.ViewModels;
using API.Repositories.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountsController : BaseController<AccountRepositories, Account, string>
{
    private AccountRepositories _repo;
    private IConfiguration _con;

    public AccountsController(AccountRepositories repo, IConfiguration con) : base(repo)
    {
        _repo = repo;
        _con = con;
    }

    [HttpPost]
    [Route("Register")]
    [AllowAnonymous]
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
    [AllowAnonymous]
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
                        new Claim("email", loginVM.Email)
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
                        expires: DateTime.Now.AddMinutes(10),
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
}

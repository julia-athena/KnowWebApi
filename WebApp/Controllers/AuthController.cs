using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApp.Models;
using WebApp.Models.Users;
using WebApp.Services.Implementations;

namespace WebApp.Controllers;

[ApiController]
[Route("api/users/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<KnowUser> _userManager;
    private readonly JwtSettings _options;

    
    public AuthController(UserManager<KnowUser> userManager, IOptions<JwtSettings> options)
    {
        _userManager = userManager;
        _options = options.Value;
    }
    
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Register(User user)
    {
        var knowUser = new KnowUser
        {
            UserName = user.Username,
            Email = user.Email
        };

        var result = await _userManager.CreateAsync(knowUser, user.Password);
        
        if (result.Succeeded)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            await _userManager.AddClaimsAsync(knowUser, claims);
            return Ok();
        }
        else return BadRequest(result.Errors);
    }
    
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<ActionResult<string>> SignIn(UserToSignIn user)
    {
        var knowUser = await _userManager.FindByEmailAsync(user.Email);
        
        var signInResult = await _userManager.CheckPasswordAsync(knowUser, user.Password);
        
        if (signInResult)
        {
            var claims = await _userManager.GetClaimsAsync(knowUser);
            var token = GetJwtToken(knowUser, claims);
            return Ok(token);
        }
        else return BadRequest();
    }

    private string GetJwtToken(KnowUser user, IEnumerable<Claim> claims)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
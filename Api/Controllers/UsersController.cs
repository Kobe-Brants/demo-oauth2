using BL.Services.User;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize("read_access")]
    public IActionResult GetUsers(CancellationToken cancellationToken)
    {
        return Ok(_userService.GetUsers(cancellationToken));
    }

    [HttpGet("{id:int}")]
    [Authorize("read_access")]
    public IActionResult GetUser(int id, CancellationToken cancellationToken)
    {
        return Ok(_userService.GetUser(id, cancellationToken));
    }

    [HttpPost]
    [Authorize("write_access")]
    public IActionResult CreateUser(User user, CancellationToken cancellationToken)
    {
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }  

    [HttpPut("{id:int}")]
    [Authorize("write_access")]
    public IActionResult UpdateUser(int id, User user, CancellationToken cancellationToken)
    {
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize("write_access")]
    public IActionResult DeleteUser(int id, CancellationToken cancellationToken)
    {
        _userService.DeleteUser(id, cancellationToken);
        return NoContent();
    }
}
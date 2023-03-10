using CUBE_Dev_App_Server_API.Models;
using CUBE_Dev_App_Server_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CUBE_Dev_App_Server_API.Controllers;

/// <summary>
/// Every requests related to connection to a client account or to the heavy client
/// </summary>
[ApiController]
[Route("[controller]")]
public class ConnectionController : ControllerBase
{
    /// <summary>
    /// Compares the incoming values with the username and password stored in the database
    /// </summary>
    [HttpGet("TryLoginHeavyClient")]
    public IActionResult TryLoginHC(string? username, string? password)
    {
        if (!LoginService.HeavyClientLogin(username, password))
            return StatusCode(400, "Incorrect username or password.");

        return Ok();
    }

    /// <summary>
    /// Changes the heavy client's username and password stored in the database with new ones if the incoming values are correct
    /// </summary>
    [HttpPut("ChangeLoginHeavyClient")]
    public IActionResult Change(string? oldUsername, string? oldPassword, string? newUsername, string? newPassword)
    {
        if (!LoginService.HeavyClientLogin(oldUsername, oldPassword))
            return StatusCode(400, "Incorrect username or password.");

        if (!LoginService.UpdateLoginInfos(newUsername, newPassword))
            return StatusCode(500, "Unable to modify the username and password.");

        return Ok();
    }

    /// <summary>
    /// Compares the incoming values with the email and password of a specified client
    /// </summary>
    /// <returns>The client ID</returns>
    [HttpGet("TryLoginWebsite")]
    public IActionResult TryLoginWS(string email, string password)
    {
        if (!LoginService.WebsiteLogin(email, password, out int idClient))
            return StatusCode(400, "Incorrect password.");

        return Ok(idClient);
    }
}

using System.Reflection;
using LoaTool.Server.Request;
using Microsoft.AspNetCore.Mvc;

namespace LoaTool.Server.Controllers;

[Route("[controller]")]
public class AuctionController : ControllerBase
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Success");
    }

    [HttpPost]
    public IActionResult Calculate([FromBody] AuctionRequest auctionRequest)
    {
        return Ok("Success");
    }
}

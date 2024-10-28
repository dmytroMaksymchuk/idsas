using IDsas.Server.DatabaseEntities;
using IDsas.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDsas.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinkController(ILinkService linkService) : ControllerBase
{
    [HttpPost("allowAccess")]
    public IActionResult AllowAccess(string linkToken, string userToken)
    {
        var linkGuid = Guid.Parse(linkToken);
        var userGuid = Guid.Parse(userToken);

        if (!linkService.OwnsLink(linkGuid, userGuid))
        {
            return Forbid();
        }

        linkService.SetAccessAllowed(linkGuid, true);

        return Ok();
    }

    [HttpPost("denyAccess")]
    public IActionResult DenyAccess(string linkToken, string userToken)
    {
        var linkGuid = Guid.Parse(linkToken);
        var userGuid = Guid.Parse(userToken);

        if (!linkService.OwnsLink(linkGuid, userGuid))
        {
            return Forbid();
        }

        linkService.SetAccessAllowed(linkGuid, false);

        return Ok();
    }
}


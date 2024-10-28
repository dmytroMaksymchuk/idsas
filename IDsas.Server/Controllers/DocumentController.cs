using IDsas.Server.Entities;
using IDsas.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDsas.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController(IDocumentService documentService) : ControllerBase
{
    /// <summary>
    /// This endpoint accepts form/multipart content in the request body.
    /// </summary>
    [HttpPost("upload")]
    public IActionResult UploadDocument(IFormFile file, string authorToken)
    {
        Guid authorGuid = Guid.Parse(authorToken);
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var (success, document) = documentService.UploadDocument(file, authorGuid);

        return Ok(document);
    }

    [HttpPut("sign")]
    public IActionResult SignDocument(string documentToken, string signerToken)
    {
        var documentGuid = Guid.Parse(documentToken);
        var signerGuid = Guid.Parse(signerToken);
        var signed = documentService.SignDocument(documentGuid, signerGuid);
        return Ok(signed);
    }

    /// <summary>
    /// Retrieve a document using its token
    /// </summary>
    [HttpGet("access")]
    public IActionResult AccessDocument(string documentToken, string userToken)
    {
        var documentGuid = Guid.Parse(documentToken);
        var userGuid = Guid.Parse(userToken);

        //First check the token against the links table
        //Check the link type if a match is found
        documentService.GetDocument(documentGuid, userGuid);
        return Ok();
    }

    /// <summary>
    /// Generate sharing link for the document
    /// </summary>
    [HttpGet("share")]
    public IActionResult ShareDocument(string documentToken, string userToken)
    {
        var documentGuid = Guid.Parse(documentToken);
        var userGuid = Guid.Parse(userToken);

        if (!documentService.OwnsDocument(userGuid))
        {
            Forbid();
        }
        return Ok(documentService.ShareDocument(documentGuid, userGuid));
    }

    [HttpGet("all")]
    public IActionResult AllDocuments(string userToken)
    {
        var userGuid = Guid.Parse(userToken);
        var (status, userDocuments) = documentService.DocumentsForUser(userGuid);
        //TODO
        throw new NotImplementedException();
    }
}

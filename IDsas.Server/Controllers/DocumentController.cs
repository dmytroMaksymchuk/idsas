using IDsas.Server.Entities;
using IDsas.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDsas.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;
    private Document _currentDocument;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
        _currentDocument = null;
    }

    /// <summary>
    /// This endpoint accepts form/multipart content in the request body.
    /// </summary>
    [HttpPost("upload")]
    public IActionResult UploadDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        Document document = _documentService.VerifyDocument(file);
        _currentDocument = document;

        return Ok(document);
    }

    [HttpPut("sign")]
    public IActionResult SignDocument(string signerName)
    {
        if (_currentDocument == null)
        {
            return BadRequest("Upload a file before signing.");
        }

        Document signed = _documentService.SignDocument(_currentDocument, signerName);

        return Ok(signed);
    }

    /// <summary>
    /// Retrieve a document using a token
    /// </summary>
    [HttpGet("document")]
    public IActionResult GetDocument(string documentId, string userToken)
    {
        //First check the token against the links table
        //Check the link type if a match is found
        _documentService.GetDocument(documentId, userToken);
        return Ok();
    }
}

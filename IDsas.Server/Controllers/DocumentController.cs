using IDsas.Server.Entities;
using IDsas.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDsas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;
        private Document _currentDocument;

        public DocumentController(IDocumentService service)
        {
            _service = service;
            _currentDocument = null;
        }

        [HttpPost("/upload")]
        public IActionResult UploadDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            Document document = _service.VerifyDocument(file);
            _currentDocument = document;

            return Ok(document);
        }

        [HttpPut("/sign")]
        public IActionResult SignDocument(string SignerName)
        {
            if (_currentDocument == null)
            {
                return BadRequest("Upload a file before signing.");
            }

            Document signed = _service.SignDocument(_currentDocument, SignerName);

            return Ok(signed);
        }

        /// <summary>
        /// Retrieve a document using a token
        /// </summary>
        [HttpGet("/document")]
        public IActionResult GetDocument(string documentToken)
        {
            //First check the token against the links table
            //Check the link type if a match is found
            _service.GetDocument(documentToken);
            return Ok();
        }


    }
}
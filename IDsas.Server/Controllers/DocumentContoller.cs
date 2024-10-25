using Microsoft.AspNetCore.Mvc;

namespace IDsas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;
        private Document? _CurrentDocument;

        public DocumentController(IDocumentService service)
        {
            _service = service;
            _CurrentDocument = null;
        }

        [HttpPost("/upload")]
        public IActionResult UploadDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            Document document = _service.VerifyDocument(file);
            _CurrentDocument = document;

            return Ok(document);
        }

        [HttpPut("/sign")]
        public IActionResult SignDocument(string SignerName)
        {
            if (_CurrentDocument == null)
            {
                return BadRequest("Upload a file before signing.");
            }

            Document signed = _service.SignDocument(_CurrentDocument, SignerName);

            return Ok(signed);
        }
    }
}
using IDsas.Server.Entities;

namespace IDsas.Server.Services;

public interface IDocumentService
{
    Document VerifyDocument(IFormFile file);

    Document SignDocument(Document document, string signerName);
}
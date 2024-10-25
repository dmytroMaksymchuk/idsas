namespace IDsas.Server;

public interface IDocumentService
{
    Document VerifyDocument(IFormFile file);

    Document SignDocument(Document document, string signerName);
}
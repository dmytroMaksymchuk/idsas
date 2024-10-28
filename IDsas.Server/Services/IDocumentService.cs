using IDsas.Server.Entities;

namespace IDsas.Server.Services;

public interface IDocumentService
{
    Document UploadDocument(IFormFile file);

    Document SignDocument(Document document, string signerName);

    Document GetDocument(Guid documentId, Guid userToken);

    string ShareDocument(string documentToken, string userToken);

    bool OwnsDocument(string userToken);

    (bool status, List<Document> userDocuments) DocumentsForUser();
}
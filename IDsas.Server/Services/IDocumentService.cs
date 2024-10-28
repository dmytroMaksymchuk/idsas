using IDsas.Server.Entities;

namespace IDsas.Server.Services;

public interface IDocumentService
{
    (bool status, Document document) UploadDocument(IFormFile file, Guid authorToken);

    Document SignDocument(Guid document, Guid signerToken);

    Document GetDocument(Guid documentId, Guid userToken);

    string ShareDocument(Guid documentToken, Guid userToken);

    bool OwnsDocument(Guid userToken);

    (bool status, List<Document> userDocuments) DocumentsForUser(Guid userToken);
}
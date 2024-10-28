using IDsas.Server.DatabaseEntities;
using IDsas.Server.RestEntities;

namespace IDsas.Server.Services;

public interface IDocumentService
{
    (bool status, DocumentResponse document) UploadDocument(IFormFile file, Guid authorToken);

    Document SignDocument(Guid document, Guid signerToken);

    DocumentEntry GetDocument(Guid documentId, Guid userToken);

    string ShareDocument(Guid documentToken, Guid userToken);

    bool OwnsDocument(Guid userToken);

    (bool status, List<DocumentEntry> userDocuments) DocumentsForUser(Guid userToken);
}
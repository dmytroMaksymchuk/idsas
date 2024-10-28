using IDsas.Server.DatabaseEntities;
using IDsas.Server.RestEntities;

namespace IDsas.Server.Services;

public interface IDocumentService
{
    (bool status, DocumentResponse document) UploadDocument(IFormFile file, Guid authorToken);

    Document SignDocument(Guid document, Guid signerToken);

    DocumentResponse GetDocument(Guid documentId, Guid userToken);
    void AccessDocument(Guid documentId, Guid userToken);

    string ShareDocument(Guid documentToken, Guid userToken, LinkType linkType);

    bool OwnsDocument(Guid documentToken, Guid userToken);

    (bool status, List<DocumentResponse> userDocuments) DocumentsForUser(Guid userToken);

    List<SharedDocumentResponse> GetDocumentsSharedWithForUser(Guid userGuid);
}
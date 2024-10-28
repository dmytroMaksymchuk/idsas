using IDsas.Server.DatabaseEntities;
using IDsas.Server.RestEntities;

namespace IDsas.Server.Services;

public class DocumentService(DatabaseContext databaseContext) : IDocumentService
{
    public (bool status, DocumentResponse document) UploadDocument(IFormFile file, Guid authorToken)
    {
        
        byte[] fileData;
        /*
        try
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            fileData = memoryStream.ToArray();
        }
        catch (IOException exception)
        {
            return (false, null);
        }*/


        // Create and persist a document entity.
        var document = new Document { Title = file.FileName, Content = [], AuthorToken = authorToken };
        databaseContext.Documents.Add(document);
        databaseContext.SaveChanges();

        // Instantiate the response body
        var documentEntry = new DocumentResponse { Content = document.Content, Title = document.Title, DocumentToken = document.Id.ToString() };
        return (true, documentEntry);
    }

    public Document SignDocument(Guid documentGuid, Guid signerGuid)
    {
        // Perform operations on the file data to actually sign the document.
        // This method returns the same file after populating the signing-related fields.
        // Save the document to the database _databaseContext.

        //TODO
        return null;
    }

    private DocumentResponse CheckUserAccess(bool confirmAssociatedUser, Guid userToken, DocumentLink d)
    {
        if (d.AssociatedUserToken is { } user)
        {
            if (user != userToken || !d.IsAssociatedUserConfirmed)
            {
                //TODO return error code when
                return null;
            }
        }
        else
        {
            d.AssociatedUserToken = userToken;
            d.IsAssociatedUserConfirmed = confirmAssociatedUser;

            // Apply the change to d
            databaseContext.DocumentLinks.Update(d);
            databaseContext.SaveChanges();
        }
        return d.Document.ToDocumentResponse();
    }

    public DocumentResponse GetDocument(Guid documentId, Guid userToken)
    {
        var d = databaseContext.DocumentLinks.First(d => d.Id == documentId);
        switch (d.LinkType)
        {
            case LinkType.Public:
                return d.Document.ToDocumentResponse();
            case LinkType.FirstToAccess:
                return CheckUserAccess(true, userToken, d);
            case LinkType.ConfirmedFirstToAccess:
                return CheckUserAccess(false, userToken, d);
            default:
                throw new ArgumentOutOfRangeException();
        }

        return null;
    }

    public void AccessDocument(Guid documentId, Guid userToken)
    {
        var d = databaseContext.DocumentLinks.First(d => d.Id == documentId);
        switch (d.LinkType)
        {
            case LinkType.Public:
                break;
            case LinkType.FirstToAccess:
                if (d.AssociatedUserToken is { } user)
                {
                    if (user != userToken || !d.IsAssociatedUserConfirmed)
                    {
                        return;
                    }
                }
                else
                {
                    d.AssociatedUserToken = userToken;
                    d.IsAssociatedUserConfirmed = false;

                    // Apply the change to d
                    databaseContext.DocumentLinks.Update(d);
                    databaseContext.SaveChanges();
                }
                break;
            case LinkType.ConfirmedFirstToAccess:
                if (d.AssociatedUserToken is { } user2)
                {
                    if (user2 != userToken || !d.IsAssociatedUserConfirmed)
                    {
                        return;
                    }
                }
                else
                {
                    d.AssociatedUserToken = userToken;
                    d.IsAssociatedUserConfirmed = true;

                    // Apply the change to d
                    databaseContext.DocumentLinks.Update(d);
                    databaseContext.SaveChanges();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public string ShareDocument(Guid documentToken, Guid userToken, LinkType linkType)
    {
        // Create a new document link entity and save it to the database.
        Document document = databaseContext.Documents.First(d => d.Id == documentToken);

        if (document.AuthorToken != userToken)
        {
            return null;
        }

        if (document == null)
        {
            return null;
        }

        // Create a new document link entity
        DocumentLink documentLink = new DocumentLink
        {
            Document = document,
            LinkType = linkType,
            IsAssociatedUserConfirmed = false
        };

        // Save the document link to the database
        databaseContext.DocumentLinks.Add(documentLink);
        databaseContext.SaveChanges();

        // Return the ID of the new document link
        return documentLink.Id.ToString();
    }

    public bool OwnsDocument(Guid documentToken, Guid userToken)
    {
        Document document = databaseContext.Documents.First(d => d.Id == documentToken);
        return document.AuthorToken == userToken;
    }

    public (bool status, List<DocumentResponse> userDocuments) DocumentsForUser(Guid userToken)
    {
        List<DocumentResponse> documents = [];

        try
        {
            var fullDocuments = databaseContext.Documents.Where(d => d.AuthorToken == userToken).ToList();

            foreach (Document doc in fullDocuments)
            {
                documents.Add(doc.ToDocumentResponse());
            }
        }
        catch (Exception e)
        {
            return (false, null);
        }

        return (true, documents);
    }

    public List<SharedDocumentResponse> GetDocumentsSharedWithForUser(Guid userGuid)
    {
        List<DocumentLink> links = databaseContext.DocumentLinks.Where(d => d.AssociatedUserToken == userGuid).ToList();
        List<SharedDocumentResponse> documentResponses = new List<SharedDocumentResponse>();

        foreach (DocumentLink link in links)
        {
            SharedDocumentResponse res = new SharedDocumentResponse
            {
                Title = link.Document.Title,
                DocumentToken = link.Document.Id.ToString(),
                available = link.LinkType == LinkType.Public || (link.IsAssociatedUserConfirmed && link.AssociatedUserToken == userGuid)
            };
            documentResponses.Add(res);
        }

        return documentResponses;
    }
}
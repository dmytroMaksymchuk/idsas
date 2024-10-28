using IDsas.Server.DatabaseEntities;
using IDsas.Server.RestEntities;

namespace IDsas.Server.Services;

public class DocumentService(DatabaseContext databaseContext) : IDocumentService
{
    public (bool status, DocumentResponse document) UploadDocument(IFormFile file, Guid authorToken)
    {
        byte[] fileData;
        try
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            fileData = memoryStream.ToArray();
        }
        catch (IOException exception)
        {
            return (false, null);
        }


        // Create and persist a document entity.
        var document = new Document { Content = fileData, AuthorToken = authorToken };
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


    public DocumentEntry GetDocument(Guid documentId, Guid userToken)
    {
        var d = databaseContext.DocumentLinks.First(d => d.Id == documentId);
        switch (d.LinkType)
        {
            case LinkType.Public:
                return d.Document.ToDocumentEntry();
            case LinkType.FirstToAccess:
                {
                    if (d.AssociatedUserToken is { } user)
                    {
                        if (user != userToken)
                        {
                            //TODO return error code when
                            return null;
                        }
                    }
                    else
                    {
                        d.AssociatedUserToken = userToken;
                        //TODO continue implementation
                    }
                    return d.Document.ToDocumentEntry();
                }
            case LinkType.ConfirmedFirstToAccess:
                {
                    //TODO implement user confirmation
                    break;
                }
            default:
                throw new ArgumentOutOfRangeException();
        }

        return null;
    }

    public string ShareDocument(Guid documentToken, Guid userToken)
    {
        return null;
    }

    public bool OwnsDocument(Guid userToken)
    {
        //TODO   
        return false;
    }

    public (bool status, List<DocumentEntry> userDocuments) DocumentsForUser(Guid userToken)
    {
        List<DocumentEntry> documents = new List<DocumentEntry>();

        try
        {
            var full_documents = databaseContext.Documents.Where(d => d.AuthorToken == userToken).ToList();

            foreach (Document doc in full_documents) {
                documents.Add(doc.ToDocumentEntry());
            }
        }
        catch (Exception e)
        {
            return (false, null);
        }

        return (true, documents);
    }
}
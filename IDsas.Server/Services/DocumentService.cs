using IDsas.Server.Entities;

namespace IDsas.Server.Services;

public class DocumentService : IDocumentService
{
    private readonly DatabaseContext _databaseContext;

    public DocumentService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Document VerifyDocument(IFormFile file)
    {
        byte[] fileData;
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            fileData = memoryStream.ToArray();
        }

        // Check file data for existing signatures
        // this method returns the uploaded documents as unsigned by default

        var document = new Document();
        return document;
    }

    public Document SignDocument(Document document, string signerName)
    {
        // Perform operations on the file data to actually sign the document.
        // This method returns the same file after populating the signing-related fields.
        // Save the document to the database _databaseContext.

        return document;
    }

    public Document GetDocument(string documentId, string userToken)
    {
        var d = _databaseContext.DocumentLinks.First(d => d.Id == Guid.Parse(documentId));
        switch (d.LinkType)
        {
            case LinkType.Public:
                return d.Document;
            case LinkType.FirstToAccess:
                {
                    if (d.AssociatedUser is { } user)
                    {
                        if (user.AuthorizationToken != Guid.Parse(userToken))
                        {
                            //TODO return error code when
                            return null;
                        }
                    }
                    else
                    {
                        d.AssociatedUser = new User { AuthorizationToken = Guid.Parse(userToken) };
                        //TODO continue implementation
                    }
                    return d.Document;
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
}
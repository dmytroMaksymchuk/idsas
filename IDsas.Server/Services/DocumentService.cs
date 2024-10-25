using IDsas.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDsas.Server.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly DocumentContext _db;

        public DocumentService(DocumentContext db)
        {
            _db = db;
        }

        public Document VerifyDocument(IFormFile file)
        {
            byte[] fileData;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                fileData = memoryStream.ToArray();
            }

            // check file data for existing signatures
            // this method returns the uploaded documents as unsigned by default

            var document = new Document
            {
            };

            return document;
        }

        public Document SignDocument(Document document, string SignerName)
        {
            // perform operations on the file data to actually sign the document
            // this method returns the same file after populating the sign-related fields 
            // save document to the database _db

            return document;
        }

        public Document GetDocument(string documentToken, string userToken)
        {
            DocumentLink d = _db.DocumentLinks.First(d => d.AccessToken == documentToken);
            switch (d.LinkType)
            {
                case LinkType.Public:
                    return d.Document;
                case LinkType.FirstToAccess:
                    {
                        if (d.AssociatedUser is { } user)
                        {
                            if (user.AuthorizationToken != userToken)
                            {
                                //TODO return error code
                                return null;
                            }
                        }
                        else
                        {
                            d.AssociatedUser = new User { AuthorizationToken = userToken };
                        }

                        return d.Document;
                        break;
                    }
                case LinkType.VerifiedFirstToAccess:
                    {

                    }
            }
        }
    }
}
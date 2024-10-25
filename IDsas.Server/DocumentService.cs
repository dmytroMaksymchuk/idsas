namespace IDsas.Server
{
    public class DocumentService : IDocumentService
    {
        // private readonly DocumentRepository _db;

        // public DocumentService(DocumentRepository db)
        // {
        //     _db = db;
        // }

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
                FileName = file.FileName,
                FileType = file.ContentType,
                FileData = fileData,
                UploadDate = DateTime.Now,
                Signed = false,
                Signer = null,
                SigningDate = null
            };

            return document;
        }

        public Document SignDocument(Document document, string SignerName)
        {
            // perform operations on the file data to actually sign the document
            // this method returns the same file after populating the sign-related fields 

            document.Signed = true;
            document.Signer = SignerName;
            document.SigningDate = DateTime.Now;

            // save document to the database _db

            return document;
        }
    }
}
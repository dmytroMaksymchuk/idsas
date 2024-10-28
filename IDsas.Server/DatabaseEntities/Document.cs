// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

using IDsas.Server.RestEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDsas.Server.DatabaseEntities;

public class Document
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public byte[] Content { get; set; }
    public Guid? AuthorToken { get; set; }
    public IList<DocumentLink> DocumentLinks { get; set; }

    public DocumentResponse ToDocumentResposend()
    {
        return new DocumentResponse
        {
            DocumentToken = Id.ToString(),
            Title = Title,
            Content = Content
        };
    }
}
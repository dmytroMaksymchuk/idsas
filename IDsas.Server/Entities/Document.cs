// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
namespace IDsas.Server.Entities;

public class Document
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public IList<DocumentLink> DocumentLinks { get; set; }
}
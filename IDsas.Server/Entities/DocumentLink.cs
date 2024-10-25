// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
namespace IDsas.Server.Entities;

public class DocumentLink
{
    public int Id { get; set; }
    public string AccessToken { get; set; }
    public Document Document { get; set; }
    
    public User AssociatedUser { get; set; }
    public LinkType LinkType { get; set; }
}
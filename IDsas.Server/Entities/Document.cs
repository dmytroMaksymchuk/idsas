// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDsas.Server.Entities;

public class Document
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public byte[] Content { get; set; }
    public User Author { get; set; }
    public IList<DocumentLink> DocumentLinks { get; set; }
}
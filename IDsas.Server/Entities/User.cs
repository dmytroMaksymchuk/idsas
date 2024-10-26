// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDsas.Server.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    public Guid AuthorizationToken { get; set; }
}
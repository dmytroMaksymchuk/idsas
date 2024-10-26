﻿// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDsas.Server.Entities;

public class DocumentLink
{
    /// <summary>
    /// Unique identifier of the document.
    /// For space efficiency, the use of a string property is generally discouraged:
    /// https://vespa-mrs.github.io/vespa.io/development/project_dev/database/DatabaseUuidEfficiency.html
    /// However, for the purposes of the Proof of Concept, it is a small sacrifice in favor of simplicity.
    /// </summary>
    ///  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }

    public Document Document { get; set; }

    public bool IsConfirmed { get; set; }
    public User AssociatedUser { get; set; }
    public LinkType LinkType { get; set; }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Models;

[Table("projectmodules")]
public partial class Projectmodule
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("projectid")]
    public int? Projectid { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [ForeignKey("Projectid")]
    [InverseProperty("Projectmodules")]
    public virtual Project? Project { get; set; }

    [InverseProperty("Module")]
    public virtual ICollection<Projectstask> Projectstasks { get; set; } = new List<Projectstask>();
}

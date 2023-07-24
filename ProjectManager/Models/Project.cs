using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Models;

[Table("projects")]
public partial class Project
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("startdate", TypeName = "date")]
    public DateTime? Startdate { get; set; }

    [Column("targetdate", TypeName = "date")]
    public DateTime? Targetdate { get; set; }

    [Column("managerid")]
    public int? Managerid { get; set; }

    [ForeignKey("Managerid")]
    [InverseProperty("Projects")]
    public virtual Employee? Manager { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Projectmodule> Projectmodules { get; set; } = new List<Projectmodule>();

    [InverseProperty("Project")]
    public virtual ICollection<Projectstask> Projectstasks { get; set; } = new List<Projectstask>();
}

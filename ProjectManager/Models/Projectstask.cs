using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Models;

[Table("projectstasks")]
public partial class Projectstask
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("projectid")]
    public int? Projectid { get; set; }

    [Column("moduleid")]
    public int? Moduleid { get; set; }

    [Column("task")]
    [StringLength(100)]
    public string? Task { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("employeeid")]
    public int? Employeeid { get; set; }

    [Column("startdate", TypeName = "date")]
    public DateTime? Startdate { get; set; }

    [Column("starttime")]
    [StringLength(50)]
    public string? Starttime { get; set; }

    [Column("enddate", TypeName = "date")]
    public DateTime? Enddate { get; set; }

    [Column("endtime")]
    [StringLength(50)]
    public string? Endtime { get; set; }

    [Column("duration")]
    public int? Duration { get; set; }

    [Column("status")]
    [StringLength(50)]
    public string? Status { get; set; }

    [ForeignKey("Employeeid")]
    [InverseProperty("Projectstasks")]
    public virtual Employee? Employee { get; set; }

    [ForeignKey("Moduleid")]
    [InverseProperty("Projectstasks")]
    public virtual Projectmodule? Module { get; set; }

    [ForeignKey("Projectid")]
    [InverseProperty("Projectstasks")]
    public virtual Project? Project { get; set; }
}

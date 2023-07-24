using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Models;

[Table("employees")]
public partial class Employee
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    [StringLength(50)]
    public string? Code { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("gender")]
    [StringLength(10)]
    public string? Gender { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("mobileno")]
    [StringLength(50)]
    public string? Mobileno { get; set; }

    [Column("password")]
    [StringLength(50)]
    public string? Password { get; set; }

    [InverseProperty("Manager")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Employee")]
    public virtual ICollection<Projectstask> Projectstasks { get; set; } = new List<Projectstask>();
}

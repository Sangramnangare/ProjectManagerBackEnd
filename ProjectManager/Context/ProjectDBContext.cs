using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Models;

namespace ProjectManager.Context;

public partial class ProjectDBContext : DbContext
{
    public ProjectDBContext()
    {
    }

    public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Projectmodule> Projectmodules { get; set; }

    public virtual DbSet<Projectstask> Projectstasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-N3MFSNE\\SQLEXPRESS;Initial Catalog=projectmanager;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasOne(d => d.Manager).WithMany(p => p.Projects).HasConstraintName("FK_projects_employees");
        });

        modelBuilder.Entity<Projectmodule>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.Projectmodules).HasConstraintName("FK_projectmodules_projects");
        });

        modelBuilder.Entity<Projectstask>(entity =>
        {
            entity.HasOne(d => d.Employee).WithMany(p => p.Projectstasks).HasConstraintName("FK_projectstasks_employees");

            entity.HasOne(d => d.Module).WithMany(p => p.Projectstasks).HasConstraintName("FK_projectstasks_projectmodules");

            entity.HasOne(d => d.Project).WithMany(p => p.Projectstasks).HasConstraintName("FK_projectstasks_projects");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

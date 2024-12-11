using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RevisaoApi.Models;

public partial class RevisaoContext : DbContext
{
    public RevisaoContext()
    {
    }

    public RevisaoContext(DbContextOptions<RevisaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("alunos");

            entity.Property(e => e.Codigo)
                .HasColumnType("int(11)")
                .HasColumnName("CODIGO");
            entity.Property(e => e.Nome)
                .HasMaxLength(500)
                .HasDefaultValueSql("''''''")
                .HasColumnName("NOME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

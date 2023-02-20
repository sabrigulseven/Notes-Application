using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Notes.Models.Entities;

public partial class NotedbContext : DbContext
{
    public NotedbContext()
    {
    }

    public NotedbContext(DbContextOptions<NotedbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;uid=root;password=godoro;database=notedb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_turkish_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("note");

            entity.HasIndex(e => e.Parentid, "parentid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("mediumtext")
                .HasColumnName("content");
            entity.Property(e => e.Parentid).HasColumnName("parentid");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.Parentid)
                .HasConstraintName("note_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

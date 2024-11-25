using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ComputerAPI.Models;

public partial class ComputerContext : DbContext
{
    public ComputerContext()
    {
    }

    public ComputerContext(DbContextOptions<ComputerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comp> Comps { get; set; }

    public virtual DbSet<Osystem> Osystems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=computer;user=root;password=;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("comp");

            entity.HasIndex(e => e.OsId, "OsId");

            entity.Property(e => e.Brand).HasMaxLength(37);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Memory).HasColumnType("int(11)");
            entity.Property(e => e.Type).HasMaxLength(30);

            entity.HasOne(d => d.Os).WithMany(p => p.Comps)
                .HasForeignKey(d => d.OsId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("comp_ibfk_1");
        });

        modelBuilder.Entity<Osystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("osystem");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

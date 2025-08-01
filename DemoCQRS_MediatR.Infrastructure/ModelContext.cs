using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DemoCQRS_MediatR.Domain.Entities;
namespace DemoCQRS_MediatR.Infrastructure;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("TEST")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Classid).HasName("SYS_C007543");

            entity.ToTable("TBL_CLASS");

            entity.Property(e => e.Classid)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("CLASSID");
            entity.Property(e => e.Classname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CLASSNAME");
            entity.Property(e => e.StudentCount)
                .HasColumnType("NUMBER")
                .HasColumnName("STUDENT_COUNT");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.SubjectId });

            entity.ToTable("tbl_mark");

            entity.HasIndex(e => e.SubjectId, "IX_tbl_mark_SubjectId");

            entity.Property(e => e.StudentId).HasPrecision(10);
            entity.Property(e => e.SubjectId).HasPrecision(10);
            entity.Property(e => e.CreateDate).HasPrecision(7);
            entity.Property(e => e.Score)
                .HasPrecision(10)
                .HasDefaultValueSql("0 ");

            entity.HasOne(d => d.Student).WithMany(p => p.Marks).HasForeignKey(d => d.StudentId);

            entity.HasOne(d => d.Subject).WithMany(p => p.Marks)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("tbl_student");

            entity.Property(e => e.StudentId).HasPrecision(10);
            entity.Property(e => e.Birthday).HasPrecision(7);
            entity.Property(e => e.ClassId).HasPrecision(10);
            entity.Property(e => e.Gender)
                .HasPrecision(10)
                .HasDefaultValueSql("0 ");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1.0 ")
                .HasColumnType("NUMBER");
            entity.Property(e => e.StudentName).HasMaxLength(250);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_STUDENT_CLASS");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId);

            entity.ToTable("tbl_subject");

            entity.Property(e => e.SubjectId).HasPrecision(10);
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1.0 ")
                .HasColumnType("NUMBER");
            entity.Property(e => e.SubjectName).HasMaxLength(200);
        });
        modelBuilder.HasSequence("STUDENT_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

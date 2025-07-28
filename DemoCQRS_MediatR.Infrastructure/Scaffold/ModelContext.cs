using DemoCQRS_MediatR.Domain.Entites;
using Microsoft.EntityFrameworkCore;
namespace DemoCQRS_MediatR.Infrastructure.Scaffold;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("TEST")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.SubjectId });

            entity.ToTable("tbl_mark");

            entity.HasIndex(e => e.SubjectId, "IX__mark_SubjectId");

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
            entity.Property(e => e.Gender)
                .HasPrecision(10)
                .HasDefaultValueSql("0 ");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1.0 ")
                .HasColumnType("NUMBER");
            entity.Property(e => e.StudentName).HasMaxLength(250);
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

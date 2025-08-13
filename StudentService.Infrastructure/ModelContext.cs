

using StudentService.Domain.AggregateModel.ClassAggregate;

namespace StudentService.Infrastructure;
public partial class ModelContext : DbContext
{
    private readonly IMediator _mediator;

    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options,IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }
    
    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ClassEntityConfiguration());
        modelBuilder
            .HasDefaultSchema("TEST")
            .UseCollation("USING_NLS_COMP");

       

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

namespace StudentService.Infrastructure.EntityConfiguration
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> studentConfiguration)
        {

                studentConfiguration.HasKey(e => e.StudentId);

                studentConfiguration.ToTable("tbl_student");

                studentConfiguration.Property(e => e.StudentId).HasPrecision(10);
                studentConfiguration.Property(e => e.Birthday).HasPrecision(7);
                studentConfiguration.Property(e => e.ClassId).HasPrecision(10);
                studentConfiguration.Property(e => e.Gender)
                    .HasPrecision(10)
                    .HasDefaultValueSql("0 ");
                studentConfiguration.Property(e => e.Status)
                    .HasDefaultValueSql("1.0 ")
                    .HasColumnType("NUMBER");
                studentConfiguration.Property(e => e.StudentName).HasMaxLength(250);

                studentConfiguration.HasOne(d => d.Class).WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_STUDENT_CLASS");
            }
        }
    }


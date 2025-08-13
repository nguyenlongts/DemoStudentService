

namespace StudentService.Infrastructure.EntityConfiguration
{
    internal class ClassEntityConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {

            builder.HasKey(e => e.ClassId).HasName("SYS_C007543");

            builder.ToTable("TBL_CLASS");

            builder.Property(e => e.ClassId)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("CLASSID");
            builder.Property(e => e.Classname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CLASSNAME");
            builder.Property(e => e.StudentCount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STUDENT_COUNT");
            ;
        }
    }
}

using APIServer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIServer.Persistence.EntityMappings
{
    public class QuestionEntityMapping : IEntityTypeConfiguration<Question>
    {
        public virtual void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");
            builder.HasKey(it => it.Id);
            builder.Property(it => it.Image_url)
                .HasMaxLength(300);
            builder.Property(it => it.QuestionText)
                .HasMaxLength(200);
            builder.HasMany(it => it.Choices)
                .WithOne(it => it.Question)
                .HasForeignKey(it => it.Id);
            builder.HasIndex(it => it.Thumb_url);
        }
    }
}

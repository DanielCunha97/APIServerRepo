using APIServer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIServer.Persistence.EntityMappings
{
    public class QuestionEntityMapping : IEntityTypeConfiguration<QuestionEntity>
    {
        public virtual void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.ToTable("Question");
            builder.HasKey(it => it.QuestionID);
            builder.Property(it => it.ImageUrl)
                .HasMaxLength(300);
            builder.Property(it => it.Question)
                .HasMaxLength(200);
            builder.HasIndex(it => it.Thumb_url);
            builder.HasMany(it => it.Choices)
                .WithOne(it => it.Question)
                .HasForeignKey(it => it.Question_ID);
           
        }
    }
}

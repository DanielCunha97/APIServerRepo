using APIServer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Persistence.EntityMappings
{
    public class ChoiceEntityMapping : IEntityTypeConfiguration<Choice>
    {
        public virtual void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.ToTable("Choice");
            builder.HasKey(it => it.Id);
            builder.Property(it => it.ChoiceText)
                .HasMaxLength(300);
            builder.Property(it => it.Votes)
                .HasMaxLength(300);
        }
    }
}

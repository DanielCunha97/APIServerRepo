using APIServer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Persistence.EntityMappings
{
    public class ChoiceEntityMapping : IEntityTypeConfiguration<ChoiceEntity>
    {
        public virtual void Configure(EntityTypeBuilder<ChoiceEntity> builder)
        {
            builder.ToTable("Choice");
            builder.HasKey(it => it.ChoiceID);
            builder.Property(it => it.Choice)
                .HasMaxLength(300);
            builder.Property(it => it.Votes)
                .HasMaxLength(300);
        }
    }
}

using APIServer.Application.Query.Providers;
using APIServer.Persistence.EntityMappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuestionEntityMapping());
            modelBuilder.ApplyConfiguration(new ChoiceEntityMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}

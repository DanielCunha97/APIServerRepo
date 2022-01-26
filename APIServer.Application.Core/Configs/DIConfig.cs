using APIServer.Application.Query.Handlers.Questions;
using APIServer.Application.Query.Providers;
using APIServer.Core.Query;
using APIServer.Persistence.Context;
using APIServer.Persistence.Query;
using APIServer.Query.Handlers.Questions.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using APIServer.Core.Commands;
using APIServer.Command.Questions.Commands;
using APIServer.Command.Questions.CommandHandlers;
using APIServer.Domain.Persistence;
using APIServer.Persistence.Repositories;
using APIServer.Persistence;
using APIServer.Command.Share.CommandHandlers;
using APIServer.Command.Share.Commands;

namespace APIServer.Application.Core.Configs
{
    public static class DIConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurePersistence(services, configuration);
            ConfigureCQRS(services);
        }

        private static void ConfigureCQRS(IServiceCollection services)
        {
            // Config our commandHandlers/queryHandlers
            services
                // Questions           
                .AddScoped<IQueryHandler<GetQuestionsQuery, Task<List<QuestionDto>>>, QuestionsQueryHandler>()
                .AddScoped<IQueryHandler<GetQuestionByIdQuery, Task<QuestionDto>>, QuestionsQueryHandler>()
                .AddScoped<ICommandHandler<CreateQuestionCommand>, QuestionsCommandHandler>()
                .AddScoped<ICommandHandler<UpdateQuestionCommand>, QuestionsCommandHandler>()
                // Share
                .AddScoped<ICommandHandler<ShareEmailCommand>, ShareCommandHandler>();
        }

        private static void ConfigurePersistence(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(
                options =>
                {
                    options
                    .UseSqlServer(configuration.GetConnectionString("DbConfig"), opt => opt.EnableRetryOnFailure(3));
                },
                ServiceLifetime.Transient);

            services.AddScoped<IQuestionProvider, QuestionProvider>();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<IShareRepository, ShareRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

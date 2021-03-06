using APIServer.Application.Query.Providers;
using APIServer.Core.Query;
using APIServer.Query.Handlers.Questions.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Application.Query.Handlers.Questions
{
    public class QuestionsQueryHandler :
        IQueryHandler<GetQuestionsQuery, Task<List<QuestionDto>>>,
        IQueryHandler<GetQuestionByIdQuery, Task<QuestionDto>>
    {
        private readonly IQuestionProvider _questionProvider;

        public QuestionsQueryHandler(IQuestionProvider questionProvider)
        {
            _questionProvider = questionProvider;
        }

        public async virtual Task<List<QuestionDto>> Handle(GetQuestionsQuery query)
        {
           Tuple<int, int, string> parameters = new Tuple<int, int, string>(query.Limit, query.Offset, query.Filter);
           var questions = await _questionProvider.GetAllAsync(parameters);
           return questions;
        }

        public async virtual Task<QuestionDto> Handle(GetQuestionByIdQuery query)
        {
            if (query.QuestionId == null || query.QuestionId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(query.QuestionId)} is required.");
            }

            var questions = await _questionProvider.GetAsync(query.QuestionId);
            return questions;
        }
    }
}

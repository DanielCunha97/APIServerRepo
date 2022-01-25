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
        IQueryHandler<GetQuestionsQuery, Task<List<QuestionDto>>>
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
    }
}

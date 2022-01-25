using APIServer.Core.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Query.Handlers.Questions.Queries
{
    public class GetQuestionByIdQuery : QueryBase
    {
        public GetQuestionByIdQuery(Guid questionId)
        {
            this.QuestionId = questionId;
        }

        public Guid QuestionId
        {
            get; set;
        }
    }
}

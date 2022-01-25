using APIServer.Domain.QuestionAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain.Persistence
{
    public interface IQuestionsRepository
    {
        Guid Add(Question questionAggregate);
        void Update(Question aggregateRoot);

        Question Get(Guid id);
    }
}

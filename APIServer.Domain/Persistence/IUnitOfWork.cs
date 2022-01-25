using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain.Persistence
{
    public interface IUnitOfWork
    {
        IQuestionsRepository QuestionsRepository
        {
            get;
        }

        void Commit();
    }
}

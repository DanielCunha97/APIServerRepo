using APIServer.Domain.Persistence;
using APIServer.Persistence.Context;
using APIServer.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dataContext;
        private readonly IQuestionsRepository _questionsRepository;    

        public UnitOfWork(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
            _questionsRepository = new QuestionsRepository(_dataContext);
        }

        public virtual IQuestionsRepository QuestionsRepository => _questionsRepository;

        public virtual void Commit()
        {
            var response = _dataContext.SaveChanges();
        }
    }
}

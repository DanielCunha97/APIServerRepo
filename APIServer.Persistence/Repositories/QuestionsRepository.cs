using APIServer.Domain.ChoiceAggregate;
using APIServer.Domain.Persistence;
using APIServer.Domain.QuestionAggregate;
using APIServer.Persistence.Context;
using APIServer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIServer.Persistence.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly ApplicationContext _dataContext;

        public QuestionsRepository(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual Guid Add(Question questionAggregate)
        {
            if (questionAggregate == null)
            {
                return Guid.Empty;
            }

            var entity = new QuestionEntity();
            entity.QuestionID = questionAggregate.Id;
            entity.Question = questionAggregate.QuestionText;
            entity.ImageUrl = questionAggregate.ImageUrl;
            entity.Thumb_url = questionAggregate.ThumbUrl;
            entity.PublishedAt = questionAggregate.PublishedAt;

            foreach (var choice in questionAggregate.Choices)
            {
                var questionChoice = Convert(choice);
                entity.Choices.Add(questionChoice);
            }

            _dataContext.Set<QuestionEntity>().Add(entity);
            var response = _dataContext.SaveChanges();
            return response > 0 ? entity.QuestionID : Guid.Empty;
        }

        public virtual void Update(Question aggregateRoot)
        {
            var entity = _dataContext
                .Set<QuestionEntity>()
                .Include(it => it.Choices)
                .FirstOrDefault(it => it.QuestionID == aggregateRoot.Id);

            if (entity == null)
                return;

            entity.Question = aggregateRoot?.QuestionText;
            entity.ImageUrl = aggregateRoot?.ImageUrl;
            entity.Thumb_url = aggregateRoot?.ThumbUrl;
            var newSetOfChoices = aggregateRoot.Choices ?? Enumerable.Empty<Choice>();
            var choicesToUpdate = entity
                .Choices
                .Where(it => newSetOfChoices.Any(p => p.ChoiceText == it.Choice))
                .ToList();
            foreach (var item in choicesToUpdate)
            {
                var choice = newSetOfChoices.FirstOrDefault(it => it.ChoiceText == item.Choice);
                item.Choice = choice.ChoiceText;
                item.Votes = item.Votes + choice.Votes;
            }

            var choicesToAdd = newSetOfChoices
                .Where(it => !entity.Choices.Any(p => p.Choice == it.ChoiceText))
                .ToList();
            foreach (var item in choicesToAdd)
            {
                entity.Choices.Add(Convert(item));
            }
            _dataContext.Update(entity);
            var response = _dataContext.SaveChanges();
        }

        public virtual Question Get(Guid id)
        {
            var entity = _dataContext
                .Set<QuestionEntity>()
                .Include(it => it.Choices)
                .FirstOrDefault(it => it.QuestionID == id);

            if (entity != null)
            {
                return Question.LoadFrom(entity);
            }
            else
            {
                return null;
            }

        }

        protected virtual ChoiceEntity Convert(Choice choice)
        {
            ChoiceEntity choiceEntity = null;

            if (choice != null)
            {
                choiceEntity = new ChoiceEntity
                {
                    ChoiceID = choice.Id,
                    Choice = choice.ChoiceText,
                    Votes = choice.Votes
                };
            }
            return choiceEntity;
        }
    }
}

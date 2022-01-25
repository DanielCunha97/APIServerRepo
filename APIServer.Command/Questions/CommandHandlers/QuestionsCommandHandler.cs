using APIServer.Command.Questions.Commands;
using APIServer.Core.Commands;
using APIServer.Domain.ChoiceAggregate;
using APIServer.Domain.Persistence;
using APIServer.Domain.QuestionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace APIServer.Command.Questions.CommandHandlers
{
    public class QuestionsCommandHandler : ICommandHandler<CreateQuestionCommand>, ICommandHandler<UpdateQuestionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsCommandHandler(
            IUnitOfWork unitOfWork,
            IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
            _unitOfWork = unitOfWork;
        }

        public void Handle(CreateQuestionCommand command)
        {
            Validate(command);
            List<Choice> choicesList = new List<Choice>();
            command.Choices.ToList().ForEach(choice =>
            {
                choicesList.Add(new Choice(choice.Trim(), 0));
            });
            var question = new Question(
                Guid.Empty,
                command.Question,
                command.ImageUrl,
                command.ThumbUrl,
                DateTime.UtcNow,
                choicesList
            );
            
            var questionIdResult = _questionsRepository.Add(question);

            var result = new CreateQuestionsCommandResult
            {
                QuestionId = questionIdResult,
            };

            command.CommandResult = result;
        }

        public void Handle(UpdateQuestionCommand command)
        {
            Validate(command);
            var question = _unitOfWork.QuestionsRepository.Get(command.Id);
            if (question != null)
            {
                var questionModel = new Question(
                    command.Id,
                    command.Question,
                    command.ImageUrl,
                    command.ThumbUrl,
                    DateTime.UtcNow,
                    command.Choices.ToList()
                );
                _questionsRepository.Update(questionModel);
            }
           
        }

        public void Validate(CreateQuestionCommand command)
        {
            if (string.IsNullOrEmpty(command.Question))
            {
                throw new ArgumentException($"{nameof(command.Question)} is required.");
            }
        }

        public void Validate(UpdateQuestionCommand command)
        {
            if (command.Id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(command.Id)} is required.");
            }
        }
    }
}

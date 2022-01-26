using APIServer.Command.Questions.CommandHandlers;
using APIServer.Command.Questions.Commands;
using APIServer.Domain.ChoiceAggregate;
using APIServer.Domain.Persistence;
using APIServer.Domain.QuestionAggregate;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace APIServer.Application.Command.Tests
{
    public class CreateQuestionCommandTests
    {
        [Fact]
        public void Should_add_Question_to_repository_correctly()
        {
            // Arrange
            var command = new CreateQuestionCommand(
                "question",
                "imageUrl",
                "thumbUrl",
               new List<string>());

            Question questionRequested = null;
            var mockedQuestionRepository = new Mock<IQuestionsRepository>();
            mockedQuestionRepository.Setup(it => it.Add(It.IsAny<Question>()))
                .Callback<Question>((it) => { questionRequested = it; });

            var handler = new QuestionsCommandHandler(null, mockedQuestionRepository.Object);

            // Action
            handler.Handle(command);

            // Assert
            Assert.Equal(command.ImageUrl, questionRequested.ImageUrl);
            Assert.Equal(command.Question, questionRequested.QuestionText);
        }
    }
}

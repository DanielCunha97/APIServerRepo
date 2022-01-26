using APIServer.Application.Query.Handlers.Questions;
using APIServer.Application.Query.Providers;
using APIServer.Query.Handlers.Questions.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APIServer.Application.Query.Tests
{
    public class GetQuestionByIdQueryTests
    {
        [Fact]
        public async Task Should_return_correctly()
        {
            // Arrange
            var query = new GetQuestionByIdQuery(Guid.NewGuid());
            var mockedQuestionQueryProvider = new Mock<IQuestionProvider>();
            var question = new QuestionDto();
            mockedQuestionQueryProvider.Setup(it => it.GetAsync(query.QuestionId))
                .Returns(Task.FromResult(question));

            var handler = new QuestionsQueryHandler(mockedQuestionQueryProvider.Object);

            // Action
            var questionResult = await handler.Handle(query);

            // Assert
            Assert.Equal(question, questionResult);
        }
    }
}

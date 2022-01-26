using APIServer.Application.Query.Providers;
using APIServer.Application.WebAPI.Models;
using APIServer.Command.Questions.Commands;
using APIServer.Core.Commands;
using APIServer.Core.Exceptions;
using APIServer.Core.Query;
using APIServer.Query.Handlers.Questions.Queries;
using APIServerRepo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APIServer.Application.WebAPI.Tests
{
    public class QuestionsControllerTests
    {
        #region PostQuestion
        [Fact]
        public async Task PostQuestion_return_BadRequest_when_request_ModelState_is_invalid()
        {
            // Arrange
            var request = new PostQuestionRequest
            {
                Choices = null,
                Image_url = string.Empty,
                Question = string.Empty,
                Thumb_url = string.Empty,
            };

            var controller = new Mock<QuestionsController>(null, null)
            {
                CallBase = true
            };
            controller.Object.ModelState.AddModelError("key", "error message");

            // Action
            var response = await controller.Object.CreateQuestion(request);

            // Assert
            Assert.NotNull(response);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.IsType<List<ErrorMessage>>(badRequestResult.Value);
        }
        #endregion PostQuestion

        #region GetQuestion
        [Fact]
        public void GetCustomer_should_return_correctly_is_null()
        {
            // Arrange
            var queryBusMock = new Mock<IQueryBus>();
            GetQuestionByIdQuery queryCalled = null;
            queryBusMock.Setup(it => it.Send<GetQuestionByIdQuery, Task<QuestionDto>>(It.IsAny<GetQuestionByIdQuery>()))
                .Callback<GetQuestionByIdQuery>((it) => { queryCalled = it; });

            var controller = new Mock<QuestionsController>(queryBusMock.Object, null)
            {
                CallBase = true
            };

            // Action
            var response = controller.Object.GetQuestion(Guid.NewGuid()).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(response);
        }

        #endregion GetQuestion
    }
}

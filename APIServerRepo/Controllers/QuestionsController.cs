using APIServer.Application.Query.Providers;
using APIServer.Application.WebAPI.Models;
using APIServer.Command.Questions.Commands;
using APIServer.Core.Commands;
using APIServer.Core.Exceptions;
using APIServer.Core.Query;
using APIServer.Domain.ChoiceAggregate;
using APIServer.Query.Handlers.Questions.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APIServerRepo.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public QuestionsController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<GenericQuestionsResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetAllQuestionsWithParameters parametersModel)
        {
            var response = await _queryBus.Send<GetQuestionsQuery, Task<List<QuestionDto>>>(new GetQuestionsQuery(parametersModel.Limit, parametersModel.Offset, parametersModel.Filter));

            if (!response.Any())
            {
                return NotFound(new ErrorMessage("validationError", "There are no Questions"));
            }

            return Ok(response);
        }

        [HttpGet("{questionId}")]
        [ProducesResponseType(typeof(GenericQuestionsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetQuestion(Guid questionId)
        {
            var response = await _queryBus.Send<GetQuestionByIdQuery, Task<QuestionDto>>(new GetQuestionByIdQuery(questionId));

            if (response == null)
            {
                return NotFound(new ErrorMessage("validationError", "There is no Question with that Id"));
            }

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericQuestionsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateQuestion([FromBody] PostQuestionRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new ErrorMessage(x.Value.Errors.First().ErrorMessage, null))
                    .ToList();

                return BadRequest(errorMessages);
            }

            var command = new CreateQuestionCommand(request.Question, request.Image_url, request.Thumb_url, request.Choices);
            _commandBus.Send(command);

            return Ok(await _queryBus.Send<GetQuestionByIdQuery, Task<QuestionDto>>(new GetQuestionByIdQuery(command.CommandResult.QuestionId)));
        }

        [HttpPut]
        [ProducesResponseType(typeof(GenericQuestionsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateQuestion([FromBody] PutQuestionRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new ErrorMessage(x.Value.Errors.First().ErrorMessage, null))
                    .ToList();

                return BadRequest(errorMessages);
            }

            List<Choice> choices = new List<Choice>();
            request.Choices.ToList().ForEach(choice =>
            {
                choices.Add(new Choice(choice.Choice, choice.Votes));
            });

            var command = new UpdateQuestionCommand(request.Id, request.Question, request.Image_url, request.Thumb_url, choices);
            _commandBus.Send(command);

            return Ok(await _queryBus.Send<GetQuestionByIdQuery, Task<QuestionDto>>(new GetQuestionByIdQuery(request.Id)));

        }
    }
}

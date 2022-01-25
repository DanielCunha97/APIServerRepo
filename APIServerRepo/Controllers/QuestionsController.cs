using APIServer.Application.Query.Providers;
using APIServer.Application.WebAPI.Models;
using APIServer.Core.Commands;
using APIServer.Core.Exceptions;
using APIServer.Core.Query;
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

        [HttpGet("test")]
        [ProducesResponseType(typeof(GetAllQuestionsWithParametersResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetAllQuestionsWithParameters parametersModel)
        {
            var response = await _queryBus.Send<GetQuestionsQuery, Task<List<QuestionDto>>>(new GetQuestionsQuery( parametersModel.Limit, parametersModel.Offset, parametersModel.Filter));

            if (!response.Any())
            {
                return NotFound(new ErrorMessage("validationError", "There are no Questions"));
            }

            return Ok(response);
        }
    }
}

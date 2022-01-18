using APIServer.Application.Core;
using APIServer.Application.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APIServerRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllQuestionsWithParametersResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetAllQuestionsWithParameters parametersModel)
        {
            var response = await _mediator.Send(new GetQuestionsQuery(requestModel.Offset, requestModel.Limit, requestModel.Filter));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}

using APIServer.Application.WebAPI.Models;
using APIServer.Command.Share.Commands;
using APIServer.Core.Commands;
using APIServer.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APIServerRepo.Controllers
{
    [Route("api/share")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public ShareController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericQuestionsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromQuery] string destination_email, [FromQuery] string content_url)
        {
            try
            {
                _commandBus.Send(new ShareEmailCommand(destination_email, content_url));
                return Ok();
            }
            catch (ArgumentNullException exception)
            {
                return BadRequest(new ErrorMessage("Error", exception.Message));
            }
        }
    }
}

using APIServer.Command.Share.Commands;
using APIServer.Core.Commands;
using APIServer.Domain;
using APIServer.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Command.Share.CommandHandlers
{
    public class ShareCommandHandler : ICommandHandler<ShareEmailCommand>
    {
        private readonly IShareRepository _shareRepository;

        public ShareCommandHandler(
            IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
        }

        public void Handle(ShareEmailCommand command)
        {
            Validate(command);
            EmailModel message = new EmailModel(null, command.DestinationEmail,"Share Email", command.ContentUrl);
            _shareRepository.SendEmailAsync(message);
        }

        public void Validate(ShareEmailCommand command)
        {
            if (string.IsNullOrEmpty(command.DestinationEmail))
            {
                throw new ArgumentException($"{nameof(command.DestinationEmail)} is required.");
            }
            if (string.IsNullOrEmpty(command.ContentUrl))
            {
                throw new ArgumentException($"{nameof(command.ContentUrl)} is required.");
            }
        }
    }
}

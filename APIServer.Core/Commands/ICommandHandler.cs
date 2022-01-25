using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Core.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        void Handle(TCommand command);

        void Validate(TCommand command);
    }

    public interface IAsyncCommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);

        void Validate(TCommand command);
    }
}

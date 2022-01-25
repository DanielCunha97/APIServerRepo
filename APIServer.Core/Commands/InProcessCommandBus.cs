using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Core.Commands
{
    public class InProcessCommandBus : ICommandBus
    {
        private readonly Func<Type, ICommandHandler> _getCommandHandler;

        public InProcessCommandBus(Func<Type, ICommandHandler> getCommandHandler)
        {
            _getCommandHandler = getCommandHandler;
        }

        public virtual void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = (ICommandHandler<TCommand>)_getCommandHandler(typeof(ICommandHandler<TCommand>));

            Contract.Assert(commandHandler != null);
            commandHandler.Handle(command);
        }

        public virtual Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = (IAsyncCommandHandler<TCommand>)_getCommandHandler(typeof(IAsyncCommandHandler<TCommand>));

            Contract.Assert(commandHandler != null);
            return commandHandler.HandleAsync(command);
        }
    }
}

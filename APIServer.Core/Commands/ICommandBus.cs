using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Core.Commands
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;

        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}

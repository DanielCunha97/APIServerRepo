using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Core.Commands
{
    public interface ICommand
    {
    }

    public interface ICommand<TCommandResult> : ICommand
    {
        TCommandResult CommandResult
        {
            get; set;
        }
    }
}

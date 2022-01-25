using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Core.Commands
{
    public class CommandBase : ICommand
    {

    }

    public abstract class CommandBase<TCommandResult> : ICommand<TCommandResult>
    {
        public TCommandResult CommandResult
        {
            get; set;
        }
    }
}

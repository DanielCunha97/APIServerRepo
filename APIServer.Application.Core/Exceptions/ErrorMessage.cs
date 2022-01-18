using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Application.Core.Exceptions
{
    public class ErrorMessage : ValueObject
    {
        public ErrorMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code
        {
            get; private set;
        }

        public string Message
        {
            get; private set;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Message;
        }
    }
}

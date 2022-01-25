using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain.ChoiceAggregate
{
    public interface IChoiceStoreObject
    {
        Guid ChoiceID
        {
            get;
        }

        string Choice
        {
            get; 
        }
        int Votes
        {
            get; 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain.ChoiceAggregate
{
    public interface IChoiceStoreObject
    {
        Guid Id
        {
            get;
        }

        string ChoiceText
        {
            get; 
        }
        int Votes
        {
            get; 
        }
    }
}

using APIServer.Domain.ChoiceAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Persistence.Entities
{
    public class Choice : IChoiceStoreObject
    {
        public Guid Id
        {
            get; set;
        }

        public string ChoiceText
        {
            get; set;
        }
        public int Votes
        {
            get; set;
        }

        public virtual Question Question
        {
            get; set;
        }
    }
}

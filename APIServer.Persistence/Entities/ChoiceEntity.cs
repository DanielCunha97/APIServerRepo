using APIServer.Domain.ChoiceAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Persistence.Entities
{
    public class ChoiceEntity : IChoiceStoreObject
    {
        public Guid ChoiceID
        {
            get; set;
        }

        public string Choice
        {
            get; set;
        }
        public int Votes
        {
            get; set;
        }

        public Guid Question_ID
        {
            get; set;
        }

        public virtual QuestionEntity Question
        {
            get; set;
        }
    }
}

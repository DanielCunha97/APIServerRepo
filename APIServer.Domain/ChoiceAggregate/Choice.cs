using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain.ChoiceAggregate
{
    public class Choice : EntityBase
    {
        public Choice(
            string choiceText,
            int votes)
        {
            this.ChoiceText = choiceText;
            this.Votes = votes;
        }

        public string ChoiceText
        {
            get; protected set;
        }

        public int Votes
        {
            get; protected set;
        }
    }
}

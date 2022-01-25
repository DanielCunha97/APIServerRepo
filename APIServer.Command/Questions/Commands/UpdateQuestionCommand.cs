using APIServer.Core.Commands;
using APIServer.Domain.ChoiceAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Command.Questions.Commands
{
    public class UpdateQuestionCommand : CommandBase
    {
        public UpdateQuestionCommand(Guid id, string question, string imageUrl, string thumbUrl, IList<Choice> choices)
        {
            this.Id = id;
            this.Question = question;
            this.ImageUrl = imageUrl;
            this.ThumbUrl = thumbUrl;
            this.Choices = new List<Choice>(choices);
        }

        public Guid Id
        {
            get; private set;
        }

        public string Question
        {
            get; private set;
        }

        public string ImageUrl
        {
            get; private set;
        }

        public string ThumbUrl
        {
            get; private set;
        }

        public IList<Choice> Choices
        {
            get; private set;
        }
    }
}

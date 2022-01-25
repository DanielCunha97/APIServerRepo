using APIServer.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Command.Questions.Commands
{
    public class CreateQuestionCommand : CommandBase<CreateQuestionsCommandResult>
    {
        public CreateQuestionCommand(string question, string imageUrl, string thumbUrl, IList<string> choices)
        {
            this.Question = question;
            this.ImageUrl = imageUrl;
            this.ThumbUrl = thumbUrl;
            this.Choices = new List<string> (choices);
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

        public IList<string> Choices
        {
            get; private set;
        }
    }
}

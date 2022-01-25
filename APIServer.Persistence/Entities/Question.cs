using APIServer.Domain.QuestionAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Persistence.Entities
{
    public class Question : IQuestionStoreObject
    {
        public Question()
        {
            this.Choices = new List<Choice>();
        }
        public Guid Id
        {
            get; set;
        }
        public string QuestionText
        {
            get; set;
        }
        public string Image_url
        {
            get; set;
        }
        public string Thumb_url
        {
            get; set;
        }
        public DateTime Published_at
        {
            get; set;
        }
        public IList<Choice> Choices
        {
            get; set;
        }
    }
}

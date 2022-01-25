using APIServer.Domain.ChoiceAggregate;
using APIServer.Domain.QuestionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIServer.Persistence.Entities
{
    public class QuestionEntity : IQuestionStoreObject
    {
        public QuestionEntity()
        {
            this.Choices = new List<ChoiceEntity>();
        }
        public Guid QuestionID
        {
            get; set;
        }
        public string Question
        {
            get; set;
        }
        public string ImageUrl
        {
            get; set;
        }
        public string Thumb_url
        {
            get; set;
        }
        public DateTime PublishedAt
        {
            get; set;
        }
        public IList<ChoiceEntity> Choices
        {
            get; set;
        }

        public IEnumerable<IChoiceStoreObject> ChoiceStoreObjects 
        {
            get
            {
                return this.Choices.OfType<IChoiceStoreObject>();
            }
        }
    }
}

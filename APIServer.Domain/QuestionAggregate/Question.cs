using APIServer.Domain.ChoiceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIServer.Domain.QuestionAggregate
{
    public class Question : EntityBase
    {
        public Question(
            Guid questionId,
            string questionText,
            string imageUrl,
            string thumbUrl,
            DateTime publishedAt,
            List<Choice> choices)
        {
            this.Id = questionId == Guid.Empty ? this.Id : questionId;
            this.QuestionText = questionText;
            this.ImageUrl = imageUrl;
            this.ThumbUrl = thumbUrl;
            this.PublishedAt = publishedAt;
            this.Choices = choices;
        }

        public string QuestionText
        {
            get; protected set;
        }

        public string ImageUrl
        {
            get; protected set;
        }

        public string ThumbUrl
        {
            get; protected set;
        }

        public DateTime PublishedAt
        {
            get; protected set;
        }

        protected Question(IQuestionStoreObject storeObject)
        {
            LoadFromStore(storeObject);
        }

        public static Question LoadFrom(IQuestionStoreObject storeObject)
        {
            if (storeObject == null)
            {
                throw new ArgumentNullException(nameof(storeObject));
            }

            return new Question(storeObject);
        }

        protected virtual void LoadFromStore(IQuestionStoreObject storeObject)
        {
            this.Id = storeObject.QuestionID;
            this.QuestionText = storeObject.Question;
            this.ImageUrl = storeObject.ImageUrl;
            this.ThumbUrl = storeObject.Thumb_url;
            this.Choices = storeObject
                .ChoiceStoreObjects
                .Select(it =>
                {
                    return new Choice(
                        it.Choice,
                        it.Votes);
                })
                .ToList();
        }

        public IEnumerable<Choice> Choices
        {
            get; protected set;
        }
    }
}

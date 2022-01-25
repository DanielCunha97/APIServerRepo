using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain.QuestionAggregate
{
    public interface IQuestionStoreObject
    {
        Guid QuestionID
        {
            get;
        }
        string Question
        {
            get;
        }
        string ImageUrl
        {
            get;
        }
        string Thumb_url
        {
            get;
        }
        DateTime PublishedAt
        {
            get; 
        }
    }
}
